using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legalacts.ECLI.Integrator.Jobs
{
    public class JobsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IJob>().To<IndexingJob>().InSingletonScope();
        }
    }
}