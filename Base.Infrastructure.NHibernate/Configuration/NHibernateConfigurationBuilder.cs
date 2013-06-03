namespace Infrastructure.NHibernate.Configuration
{
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using global::NHibernate.Cfg;

    public class NHibernateConfigurationBuilder
    {
        private readonly string _connectionStringKey;

        private readonly Assembly _mappingsAssembly;

        public NHibernateConfigurationBuilder(string connectionStringKey, Assembly mappingsAssembly)
        {
            _connectionStringKey = connectionStringKey;
            _mappingsAssembly = mappingsAssembly;
        }

        public Configuration Build()
        {
            // Setting common configuration
            var fluentConfig = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(
                        builder => builder.FromConnectionStringWithKey(_connectionStringKey)))
                    .ExposeConfiguration(ApplyAdditionalConfiguration);

            // Applying bounded context related mappings
            fluentConfig.Mappings(c => 
                c.FluentMappings.AddFromAssembly(_mappingsAssembly)
#if DEBUG
                    // you can find nhibernate mappings exported to xml at bin\mappings
                    .ExportMappings()
#endif
            );
            // Building and returning NH configuration
            var config = fluentConfig.BuildConfiguration();
            return config;
        }

        private void ApplyAdditionalConfiguration(Configuration c)
        {
            c.SetProperty(global::NHibernate.Cfg.Environment.UseProxyValidator, bool.FalseString);
#if DEBUG
            // write generated sql to the VS Output
            c.SetInterceptor(new SqlStatementInterceptor());
#endif
        }
    }
}
