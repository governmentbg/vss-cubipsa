using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legalacts.Model.UnitOfWork;
using Legalacts.Model.Entities;
using Ninject;

namespace Legalacts.Model.Repositories
{
    public class RepositoryBase : IRepository
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        internal LegalactsContext DataContext
        {
            get
            {
                return ((UnitOfWorkImpl)UnitOfWork).DataContext;
            }
        }
    }
}
