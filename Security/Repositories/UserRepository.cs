namespace Security.Repositories
{
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    using Infrastructure.NHibernate.Repositories;

    using NHibernate;

    using Security.Domain;

    [DomainRepositoryImplementation]
    public class UserRepository : GenericRepositoryForBaseEntity<User>, IUserRepository
    {
        public UserRepository(ISession session, InjectorHelper injectorHelper)
            : base(session, injectorHelper)
        {
        }
    }
}
