using System.Data;

namespace Legalacts.Model.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITransaction BeginTransaction();
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
        void Save();
    }
}
