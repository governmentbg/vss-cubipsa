using Legalacts.Model.Repositories;
using Ninject;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Legalacts.ECLI.Integrator.Controllers
{
    public class AllowSundayFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var now = DateTime.Now;
            TimeSpan start = new TimeSpan(0, 0, 0);
            TimeSpan end = new TimeSpan(6, 0, 0);

            var inAllowedTimeRange = (now.TimeOfDay > start) && (now.TimeOfDay < end);

            if (now.DayOfWeek != DayOfWeek.Sunday || !inAllowedTimeRange)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(actionContext);
        }
    }

    public class FileController : ApiController
    {
        [Inject]
        public ILegalactsRepository _legalactsRepository { get; set; }

        public FileController() { }

        [AllowSundayFilter]
        [Route("{ecli:regex(^ECLI:BG:[A-Z]{2}[0-9]{3}:[0-9]{4}:[0-9]{11}.[0-9]{3}$)}/{type:regex((?:^|\\W)act(?:$|\\W)|(?:^|\\W)motive(?:$|\\W))}"), HttpGet]
        public HttpResponseMessage Get(string ecli, string type)
        {
            var act = _legalactsRepository.GetActByEcli(ecli);

            var isModeMotive = type == "motive";

            if (isModeMotive)
            {
                if (act?.MotiveDocument?.Content == null || string.IsNullOrWhiteSpace(act.MotiveDocument.MimeType))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            else
            {
                if (act?.ActDocument?.Content == null || string.IsNullOrWhiteSpace(act.ActDocument.MimeType))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }

            var mimeType = isModeMotive ? act.MotiveDocument.MimeType : act.ActDocument.MimeType;
            byte[] fileContent = Utils.Managers.ZipManager.Decompress(isModeMotive ? act.MotiveDocument.Content : act.ActDocument.Content);

            string fileName = isModeMotive ? "motive" : "act";
            switch (mimeType)
            {
                case "text/html":
                    fileName += ".htm";
                    break;
                case "application/msword":
                    fileName += ".doc";
                    break;
                case "text/plain":
                    fileName += ".txt";
                    break;
                case "application/pdf":
                    fileName += ".pdf";
                    break;
                default:
                    break;
            }

            var stream = new MemoryStream(fileContent);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName,
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue(mimeType);

            result.Content.Headers.ContentEncoding.Add("windows-1251");

            return result;
        }
    }
}
