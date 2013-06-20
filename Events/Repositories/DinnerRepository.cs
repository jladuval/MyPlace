namespace Events.Repositories
{
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    using Events.Domain;

    using Infrastructure.NHibernate.Repositories;

    using NHibernate;

    [DomainRepositoryImplementation]
    public class DinnerRepository : AggregateRepository<Dinner>, IDinnerRepository
    {
        public DinnerRepository(ISession session, InjectorHelper injectorHelper)
            : base(session, injectorHelper)
        {
        }
    }
}
