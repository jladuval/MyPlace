namespace Web
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Base.StorageQueue;

    using Castle.Windsor;

    using Common.DI;

    using Security.Interfaces.Application.Impersonation;

    using Web.Migrations;

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
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
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
        }

        private static void InitializeInfrastructure()
        {
            var storageQueues = container.Resolve<IStorageQueues>();
            storageQueues.InitializeAllQueues();
        }

        private static void MigrateDatabaseSchema()
        {
            var migrator = container.Resolve<Migrator>();
            migrator.MigrateUp();
        }
    }
}