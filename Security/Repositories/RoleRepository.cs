namespace Security.Repositories
{
    using System.Linq;

    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    using Infrastructure.NHibernate.Repositories;

    using NHibernate;
    using NHibernate.Linq;

    using Security.Domain;

    [DomainRepositoryImplementation]
    public class RoleRepository : GenericRepositoryForBaseEntity<Role>, IRoleRepository
    {
        public RoleRepository(ISession session, InjectorHelper injectorHelper)
            : base(session, injectorHelper)
        {
        }

        public Role LoadByName(string roleName)
        {
            var role = Session.Query<Role>().FirstOrDefault(x => x.Name == roleName);
            return role;
        }
    }
}
