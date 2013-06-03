#if DEBUG
namespace Infrastructure.NHibernate.Configuration
{
    using FluentNHibernate.Cfg;

    internal static class FluentMappingsContainerDebugExtensions
    {
        private static readonly string ExportPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Mappings");
 
        public static FluentMappingsContainer ExportMappings(this FluentMappingsContainer container)
        {
            if (!System.IO.Directory.Exists(ExportPath))
            {
                System.IO.Directory.CreateDirectory(ExportPath);
            }

            container.ExportTo(ExportPath);
            return container;
        }
    }
}
#endif
