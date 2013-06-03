namespace Infrastructure.NHibernate.Repositories
{
    using System;
    using System.Linq;

    using Infrastructure.NHibernate.Extensions;

    using Base.DDD.Domain;
    using Base.DDD.Domain.Support;

    using global::NHibernate;

    public class GenericRepositoryForBaseEntity<TEntity> : GenericRepository<TEntity, Guid>
        where TEntity : Entity
    {
        private readonly InjectorHelper injectorHelper;

        public GenericRepositoryForBaseEntity(ISession session, InjectorHelper injectorHelper)
            : base(session)
        {
            this.injectorHelper = injectorHelper;
        }

        #region CRUD operations

        public override TEntity Load(Guid id)
        {
            var entity = base.Load(id);

            var aggregateRoot = entity as AggregateRoot;

            // Or we could just make AggregateRoot.EventPublisher getter public and check whether is already assigned
            var containsKey = Session
                .GetLocalKeys<TEntity>()
                .Contains(id);

            if (aggregateRoot != null && !containsKey)
            {
                // Inject aggregate root services
                this.injectorHelper.InjectDependencies((AggregateRoot)(object)entity);
            }

            return entity;
        }

        public override void Save(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            base.Save(entity);
        }

        #endregion
    }
}