using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Legalacts.Utils;

namespace Legalacts.ECLI.Integrator
{
    public class IpFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var inAllowedTimeRange = true;

            try
            {
                var ip = IPAddress.Parse(((HttpContextBase)actionContext.Request
                                    .Properties["MS_HttpContext"]).Request.UserHostAddress);

                var whitelistIps = ConfigurationManager.AppSettings["WhitelistIps"].Split(',');
                var whitelistMasks = ConfigurationManager.AppSettings["WhitelistMasks"].Split(',');

                var isInIpList = whitelistIps.Contains(ip.ToString());
                var isInIpMasks = false;

                foreach(var m in whitelistMasks)
                {
                    if (ip.IsInSubnet(m))
                    {
                        isInIpMasks = true;
                        break;
                    }
                }

                inAllowedTimeRange = isInIpList || isInIpMasks;
            }
            catch { }

            if (!inAllowedTimeRange)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}