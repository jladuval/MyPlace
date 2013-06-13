﻿namespace Security.Repositories
{
    using System;

    using Base.DDD.Domain.Annotations;

    using Security.Domain;

    [DomainRepository]
    public interface IRoleRepository
    {
        void Save(Role role);

        Role Load(Guid roleId);

        Role LoadByName(string roleName);
    }
}
