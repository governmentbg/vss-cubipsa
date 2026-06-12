using Legalacts.Web.Utils;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Legalacts.Web.App_Start
{
    public static class IpMiddlewareExtension
    {
        public static void UseIpMiddleware(this IAppBuilder app)
        {
            app.Use<IpMiddleware>();
        }
    }

    public class IpMiddleware : OwinMiddleware
    {
        private static DateTime _dictionaryIssueDate;
        private static List<IpLimit> _WhiteListIpds = null;
        private static ConcurrentDictionary<string, IpStatus> _IpsCounter = null;

        public IpMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                LoadWhiteListIps();
                InitDictionary();

                if (context.Request.Path.HasValue && context.Request.Path.Value.EndsWith("/ip-status"))
                {
                    ShowStatistics(context);
                    return;
                }

                if (context.Request.Path.HasValue && context.Request.Path.Value.EndsWith("/load-whitelist"))
                {
                    _WhiteListIpds = null;
                    LoadWhiteListIps();
                    return;
                }

                var ipAddress = (string)context.Request.Environment["server.RemoteIpAddress"];
                var ipLimit = _WhiteListIpds.FirstOrDefault(e => e.IP == ipAddress);

                if (ipLimit == null)
                {
                    ipLimit = new IpLimit
                    {
                        IP = ipAddress,
                        Limit = Statics.AccessDenyLimit
                    };
                }

                if (_IpsCounter.Keys.Contains(ipAddress))
                {
                    IpStatus status = null;
                    _IpsCounter.TryGetValue(ipAddress, out status);

                    var newStatus = new IpStatus(status.AccessCount + 1, ipLimit.Limit);

                    _IpsCounter.TryUpdate(ipAddress, newStatus, status);

                    if (status.IsBlocked)
                    {
                        context.Response.StatusCode = 403;
                        return;
                    }
                }
                else
                {
                    var newStatus = new IpStatus(1, ipLimit.Limit);

                    _IpsCounter.TryAdd(ipAddress, newStatus);
                }
            }
            catch { }

            await Next.Invoke(context);
        }

        private void LoadWhiteListIps()
        {
            if (_WhiteListIpds == null)
            {
                _WhiteListIpds = new List<IpLimit>();

                var path = Path.Combine(Directory.GetParent(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().EscapedCodeBase.Substring(8))).FullName, "whitelist.json");

                var file = File.ReadAllText(path);

                var whiteList = JsonConvert.DeserializeObject<List<IpLimit>>(file);

                if (whiteList != null && whiteList.Count > 0)
                {
                    _WhiteListIpds.AddRange(whiteList);
                }
            }
        }

        private void InitDictionary()
        {
            if (_IpsCounter == null)
            {
                _IpsCounter = new ConcurrentDictionary<string, IpStatus>();
                _dictionaryIssueDate = DateTime.Now;
            }
            else
            {
                if (DateTime.Now.Subtract(_dictionaryIssueDate).TotalDays >= Statics.AccessDenyLimitDurationInDays)
                {
                    _IpsCounter.Clear();
                    _dictionaryIssueDate = DateTime.Now;
                }
            }
        }

        private void ShowStatistics(IOwinContext context)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Info:");
            sb.AppendLine($"- Unique IPs: {_IpsCounter.Count}");
            sb.AppendLine($"- Whitelist IPs: {_WhiteListIpds.Count()}");
            sb.AppendLine($"- Blocked IPs: {_IpsCounter.Where(e => e.Value.IsBlocked).Count()}");
            sb.AppendLine($"- IssueDate: {_dictionaryIssueDate}");
            sb.AppendLine($"- AccessDenyLimit: {Statics.AccessDenyLimit}");
            sb.AppendLine($"- AccessDenyLimitDurationInDays: {Statics.AccessDenyLimitDurationInDays}");
            sb.AppendLine($"- Days since last clear: {DateTime.Now.Subtract(_dictionaryIssueDate).TotalDays}");
            sb.AppendLine();

            sb.AppendLine("Blocked Ips:");
            var restrictedIps = string.Join(Environment.NewLine, _IpsCounter.Where(e => e.Value.IsBlocked).OrderByDescending(e => e.Value.AccessCount));
            sb.AppendLine(string.IsNullOrWhiteSpace(restrictedIps) ? "-" : restrictedIps);
            sb.AppendLine();

            sb.AppendLine("Whitelist Ips:");
            var whitelistIps = string.Join(Environment.NewLine, _IpsCounter.Where(e => _WhiteListIpds.Any(w => w.IP == e.Key)).OrderByDescending(e => e.Value.AccessCount));
            sb.AppendLine(string.IsNullOrWhiteSpace(whitelistIps) ? "-" : whitelistIps);
            sb.AppendLine();

            sb.AppendLine("All other Ips:");
            var allOtherIps = string.Join(Environment.NewLine, _IpsCounter.Where(e => !e.Value.IsBlocked).OrderByDescending(e => e.Value.AccessCount));
            sb.AppendLine(string.IsNullOrWhiteSpace(allOtherIps) ? "-" : allOtherIps);

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";
            context.Response.Write($"<pre>{sb}</pre>");
        }
    }

    internal class IpStatus
    {
        public IpStatus(int accessCount, int accessLimit)
        {
            this.AccessCount = accessCount;
            this.AccessLimit = accessLimit;
            this.LastAccess = DateTime.Now;
        }

        public int AccessCount { get; private set; }
        public int AccessLimit { get; private set; }
        public DateTime LastAccess { get; private set; }
        public bool IsBlocked
        {
            get
            {
                return AccessCount > AccessLimit;
            }
        }

        public override string ToString()
        {
            return $"AccessCount: {AccessCount}, AccessLimit: {AccessLimit}, LastAccess: {LastAccess}";
        }
    }

    internal class IpLimit
    {
        public string IP { get; set; }
        public int Limit { get; set; }
    }
}