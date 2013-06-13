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

        #region CRUD operations

        public virtual TEntity Load(TKey id)
        {
            var entity = Session.Get<TEntity>(id);
            return entity;
        }

        /// <summary>
        /// Deletes the entity from local storage
        /// </summary>
        /// <param name="id">Entity identifier</param>
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

        #endregion
    }
}
