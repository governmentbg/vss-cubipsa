using NLog;
using System.Web.Mvc;
namespace Legalacts.Web.Filters
{
    public class NLogExceptionFilter : HandleErrorAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext filterContext)
        {
            Logger.Error(string.Empty, filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}
