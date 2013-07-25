using System.Configuration;
using FluentMigrator.InProc;

namespace Web
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using App_Start;
    using Base.StorageQueue;

    using Castle.Windsor;

    using Common.DI;

    using Core.Impersonation;

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container { get; set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var identityProvider = container.Resolve<IIdentityProvider>();
            identityProvider.ProcessIdentity(this);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            container = ContainerInit.RegisterDI(Assembly.GetExecutingAssembly());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            InitializeInfrastructure();
            MigrateDatabaseSchema();
            AutoMapping.CreateMappings();
        }

        private static void InitializeInfrastructure()
        {
            var storageQueues = container.Resolve<IAzureStorage>();
            storageQueues.InitializeStorage();
        }

        private static void MigrateDatabaseSchema()
        {
            new Migrator(new MigratorContext
            {
                Connection = ConfigurationManager.ConnectionStrings["db"].ConnectionString,
                Database = "sqlserver2008",
                MigrationsAssembly = typeof(MigratorContext).Assembly,
            }).MigrateUp();
        }
    }
}