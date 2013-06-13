namespace Security.Repositories
{
    using Base.DDD.Domain.Annotations;

    using Security.Domain;

    [DomainRepository]
    public interface IUserRepository
    {
        void Save(User user);
    }
}
