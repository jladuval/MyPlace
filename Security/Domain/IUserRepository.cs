using Base.DDD.Domain.Annotations;

namespace Security.Domain
{
    [DomainRepository]
    public interface IUserRepository
    {
        void Save(User user);
    }
}
