using Legalacts.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Legalacts.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new NLogExceptionFilter());
            filters.Add(new NLogTraceFilter());
        }
    }
}
