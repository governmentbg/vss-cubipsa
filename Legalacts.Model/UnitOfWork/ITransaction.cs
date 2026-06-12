using System;

namespace Legalacts.Model.UnitOfWork
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
