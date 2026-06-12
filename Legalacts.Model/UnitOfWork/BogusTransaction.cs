namespace Legalacts.Model.UnitOfWork
{
    internal class BogusTransaction : ITransaction
    {
        public void Commit()
        {
        }

        public void Rollback()
        {
        }

        public void Dispose()
        {
        }
    }
}
