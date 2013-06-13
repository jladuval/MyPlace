namespace Accounts.Repositories
{
    using System;

    using Accounts.Domain;

    using Base.DDD.Domain.Annotations;

    [DomainRepository]
    public interface IUserRepository
    {
        void Save(User user);

        User Load(Guid id);
    }
}
