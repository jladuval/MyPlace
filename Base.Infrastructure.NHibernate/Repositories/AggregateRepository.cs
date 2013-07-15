namespace Infrastructure.NHibernate.Repositories
{
    using System;
    using System.Linq;

    using Base.DDD.Domain;
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    using Infrastructure.NHibernate.Extensions;

    using global::NHibernate;

    [DomainService]
    public class AggregateRepository<TEntity> : Repository<TEntity, Guid>, IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly InjectorHelper _injectorHelper;

        public AggregateRepository(ISession session, InjectorHelper injectorHelper)
            : base(session)
        {
            _injectorHelper = injectorHelper;
        }

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
                _injectorHelper.InjectDependencies(aggregateRoot);
            }

            return entity;
        }

        public override void Save(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            base.Save(entity);
        }
    }
}