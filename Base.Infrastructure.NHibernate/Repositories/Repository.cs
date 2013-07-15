namespace Infrastructure.NHibernate.Repositories
{
    using System.Linq;

    using global::NHibernate;
    using global::NHibernate.Linq;

    public class Repository<TEntity, TKey> where TEntity : class
    {
        public Repository(ISession session)
        {
            Session = session;
        }

        protected ISession Session { get; private set; }

        public virtual TEntity Load(TKey id)
        {
            var entity = Session.Get<TEntity>(id);
            return entity;
        }

        public virtual void Delete(TKey id)
        {
            var entity = Load(id);
            Session.Delete(entity);
        }

        public virtual void Save(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public virtual IQueryable<TEntity> Find()
        {
            return Session.Query<TEntity>();
        } 
    }
}
