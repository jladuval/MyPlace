namespace Accounts.Repositories
{
    using Accounts.Domain;

    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    using Infrastructure.NHibernate.Repositories;

    using NHibernate;

    [DomainRepositoryImplementation]
    public class UserRepository : AggregateRepository<User>, IUserRepository
    {
        public UserRepository(ISession session, InjectorHelper injectorHelper)
            : base(session, injectorHelper)
        {
        }
    }
}
