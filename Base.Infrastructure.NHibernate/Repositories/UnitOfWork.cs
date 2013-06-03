namespace Infrastructure.NHibernate.Repositories
{
    using System;
    using System.Transactions;

    public class UnitOfWork : IDisposable
    {
        private readonly TransactionScope _transaction;

        public UnitOfWork()
        {
            _transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
        }

        public void Dispose()
        {
            _transaction.Complete();
            _transaction.Dispose();
        }
    }
}
