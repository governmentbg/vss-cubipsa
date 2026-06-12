using Legalacts.Model.Entities;
using System;
using System.Data;

namespace Legalacts.Model.UnitOfWork
{
    public class UnitOfWorkImpl : IUnitOfWork, IDisposable
    {
        #region Public

        public UnitOfWorkImpl()
        {
            DataContext = new LegalactsContext();
            DataContext.Database.CommandTimeout = 180;
            _disposed = false;
        }

        public void Save()
        {
            DataContext.SaveChanges();
        }        

        public ITransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            // if the connection is closed there is no current transaction
            // otherwise there is and we are trying to create a nested transaction
            // so we create a bogus transaction that does nothing
            if (DataContext.Database.Connection.State == ConnectionState.Closed)
            {
                DataContext.Database.Connection.Open();
                _lastIsolationLevel = isolationLevel;
                return new Transaction(DataContext.Database.Connection, DataContext.Database.Connection.BeginTransaction(isolationLevel));
            }
            else
            {
                if ((int)isolationLevel > (int)_lastIsolationLevel)
                    throw new InvalidOperationException("There is a parent transaction with lower isolation level.");

                return new BogusTransaction();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (DataContext != null)
                        DataContext.Dispose();
                }

                DataContext = null;
                _disposed = true;
            }
        }

        #endregion

        #region Internal

        internal LegalactsContext DataContext { get; private set; }

        #endregion

        #region Private

        private bool _disposed;
        private IsolationLevel _lastIsolationLevel = IsolationLevel.Unspecified;

        #endregion
    }
}
