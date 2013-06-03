namespace Security.Domain
{
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    [DomainFactory]
    public class UserFactory
    {
        public InjectorHelper Helper { get; set; }

        public User CreateUser(string email, string password, string salt)
        {
            var user = new User(email, password, salt);
            Helper.InjectDependencies(user);
            return user;
        }

    }
}
