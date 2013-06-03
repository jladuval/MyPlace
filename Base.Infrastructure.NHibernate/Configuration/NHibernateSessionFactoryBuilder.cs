namespace Infrastructure.NHibernate.Configuration
{
    using global::NHibernate;
    using global::NHibernate.Cfg;

    public class NHibernateSessionFactoryBuilder
    {
        private readonly Configuration _configuration;

        public NHibernateSessionFactoryBuilder(Configuration configuration)
        {
            _configuration = configuration;
        }

        public ISessionFactory Build()
        {
            var sessionFactory = _configuration.BuildSessionFactory();
            return sessionFactory;
        }
    }
}