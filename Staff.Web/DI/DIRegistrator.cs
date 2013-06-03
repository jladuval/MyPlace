namespace CQRS.Staff.Web.DI
{
    using System.Reflection;

    using CQRS.Common.DI;
    using CQRS.Security.Application.Services;

    public static class DIRegistrator
    {
        public static void Register()
        {
            var webAssembly = Assembly.GetExecutingAssembly();
            ContainerInit.RegisterDI(webAssembly, GetEntitiesAssemblies);
        }

        private static Assembly[] GetEntitiesAssemblies()
        {
            var assemblies = new[]
                {
                    typeof(CryptoService).Assembly, 
                };

            return assemblies;
        }
    }
}