using Legalacts.Model;
using Legalacts.Utils.Communicators;
using Legalacts.Web.Core;
using Legalacts.Web.Jobs;
using Ninject;
using Ninject.Web.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Legalacts.Web
{
    public class MvcApplication : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            LogManager.ThrowExceptions = true;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            foreach (var job in CreateKernel().GetAll<IJob>())
            {
                job.Start();
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            foreach (var job in CreateKernel().GetAll<IJob>())
            {
                job.Dispose();
            }
        }

        protected IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
        
            kernel.Load(new LegalactsModelModule());
            kernel.Load(new JobsModule());

            return kernel;
        }
    }
}
