namespace Infrastructure.Configuration
{
    using System.Configuration;

    public class ApplicationSettings : IPersistenceSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["db"].ConnectionString; }
        }

        public string this[string name]
        {
            get { return ConfigurationManager.AppSettings[name]; }
        }
    }
}