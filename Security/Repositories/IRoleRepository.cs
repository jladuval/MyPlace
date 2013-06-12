namespace Security.Domain
{
    using System;

    using Base.DDD.Domain.Annotations;

    [DomainRepository]
    public interface IRoleRepository
    {
        void Save(Role role);

        Role Load(Guid roleId);

        Role LoadByName(string roleName);
    }
}
