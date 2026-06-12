using System;
using System.Data.Common;

namespace Legalacts.Model.UnitOfWork
{
    internal class Transaction : ITransaction, IDisposable
    {
        private bool _disposed = false;
        private bool _commitedOrRolledBack = false;
        private DbConnection _innerConnection;
        private DbTransaction _innerTransaction;

        internal Transaction(DbConnection innerConnection, DbTransaction innerTransaction)
        {
            _innerConnection = innerConnection;
            _innerTransaction = innerTransaction;
        }

        public void Commit()
        {
            _innerTransaction.Commit();
            _commitedOrRolledBack = true;
        }

        public void Rollback()
        {
            _innerTransaction.Rollback();
            _commitedOrRolledBack = true;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (!_commitedOrRolledBack)
                {
                    _innerTransaction.Rollback();
                }

                _innerConnection.Close();

                _disposed = true;
            }
        }
    }
}
