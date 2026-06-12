using Legalacts.ECLI.Integrator.Jobs;
using Legalacts.Model;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Ninject;
using System;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;

namespace Legalacts.ECLI.Integrator
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configuration.Filters.Add(new IpFilter());

            GlobalConfiguration.Configuration.MessageHandlers
                .Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            GlobalConfiguration.Configure(WebApiConfig.Register);

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
