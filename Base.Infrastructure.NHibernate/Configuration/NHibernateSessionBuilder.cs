namespace Infrastructure.NHibernate.Configuration
{
    using global::NHibernate;

    public class NHibernateSessionBuilder
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateSessionBuilder(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession Build()
        {
            var session = _sessionFactory.OpenSession();
            return session;
        }
    }
}