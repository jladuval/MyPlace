namespace Infrastructure.NHibernate.Repositories
{
    using System;
    using System.Linq;

    public interface IRepository<T> where T : class
    {
        T Load(Guid id);

        void Delete(Guid id);

        void Save(T entity);

        IQueryable<T> Find();
    }
}
