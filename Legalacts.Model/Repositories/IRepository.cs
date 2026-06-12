using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legalacts.Model.UnitOfWork;

namespace Legalacts.Model.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}
