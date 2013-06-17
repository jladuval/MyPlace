namespace Events.Repositories
{
    using System;

    using Base.DDD.Domain.Annotations;

    using Events.Domain;

    [DomainRepository]
    public interface IUserRepository
    {
        void Save(User user);

        User Load(Guid id);
    }
}
