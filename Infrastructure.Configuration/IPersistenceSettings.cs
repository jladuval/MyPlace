namespace Infrastructure.Configuration
{
    public interface IPersistenceSettings
    {
        string ConnectionString { get; }
    }
}
