using Legalacts.Model.Repositories;
using Legalacts.Model.Repositories.SitemapRepository;
using Legalacts.Model.UnitOfWork;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalacts.Model
{
    public class LegalactsModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWorkImpl>().InRequestScope();

            Bind<INomenclatureRepository>().To<NomenclatureRepository>().InRequestScope();
            Bind<ILegalactsRepository>().To<LegalactsRepository>().InRequestScope();

            Bind<ISitemapRepository>().To<SitemapRepository>().InRequestScope();
        }
    }
}
