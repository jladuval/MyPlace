namespace Common.DI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using Base.CQRS.Commands.Decorator;
    using Base.CQRS.Commands.Handler;
    using Base.CQRS.Query.Attributes;
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Infrastructure.Events;
    using Base.DDD.Infrastructure.Events.Implementation;
    using Base.Infrastructure.Attributes;

    using Castle.Core;
    using Castle.Facilities.Startable;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;

    using Infrastructure.Configuration;
    using Infrastructure.NHibernate.Configuration;
    using Infrastructure.NHibernate.Conventions;

    using NHibernate;

    using Security.Services;

    using global::NHibernate.Cfg;

    public class ContainerInit
    {
        public static IWindsorContainer RegisterDI(Assembly webAssembly)
        {
            IWindsorContainer container = new WindsorContainer();

            AddResolversAndFacilities(container);
            RegisterMvcControllers(container, webAssembly);

            // Temp
            RegisterEventPublishers(container);

            // Register event component listeners
            // This line resolves IEventSubscriber
            container.AddFacility<SubscribeEventListenerFacility>();

            RegisterDomain(container);            
            RegisterSingletons(container);
            RegisterORM(container);            
            RegisterStatefullComponents(container, webAssembly);
            RegisterEventListeners(container);
            RegisterCommandHandlers(container);
            RegisterConfiguration(container);
            SetMvcContainer(container);
            return container;
        }

        private static void RegisterEventPublishers(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyContaining<SimpleEventPublisher>()
                    .Where(x => x == typeof(SimpleEventPublisher))
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton());
        }

        private static void RegisterSingletons(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
                    .Where(t => t.IsComponentLifestyle(ComponentLifestyle.Singleton))
                    .StartIfNecessary()
                    .WithServiceAllInterfaces()
                    .WithServiceSelf()
                    .LifestyleSingleton());
        }

        private static void RegisterDomain(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
                    .Where(t => t.IsComponentLifestyle(ComponentLifestyle.Transient) 
                        || t.IsDefined(typeof(DomainFactoryAttribute), true)
                        || t.IsDefined(typeof(DomainRepositoryImplementationAttribute), true)
                        || t.IsDefined(typeof(DomainServiceAttribute), true) 
                        || t.IsDefined(typeof(ReaderAttribute), true))
                    .WithServiceAllInterfaces()
                    .WithServiceSelf()
                    .LifestyleTransient());
        }

        private static Assembly[] GetAssemblies()
        {
            return new[]
                {
                    typeof(CryptoService).Assembly, 
                };
        }

        private static void RegisterORM(IWindsorContainer container)
        {
            AutomappingConfiguration.IsEntityPredicate = t =>
                t.IsDefined(typeof(DomainEntityAttribute), true);

            AutomappingConfiguration.IsComponentPredicate =
                t => t.IsDefined(typeof(DomainValueObjectAttribute), true);

            container.Register(Component.For<ISession>());
        }

        private static void RegisterMvcControllers(IWindsorContainer container, Assembly webAssembly)
        {
            container.Register(Classes.FromAssembly(webAssembly)
                                   .BasedOn<IController>()
                                   .LifestyleTransient());
        }

        private static void SetMvcContainer(IWindsorContainer container)
        {
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static void RegisterStatefullComponents(IWindsorContainer container, Assembly webAssembly)
        {
            container.Register(Classes.FromAssembly(webAssembly)
                                   .Where(t => t.IsComponentLifestyle(ComponentLifestyle.PerSession))
                                   .WithServiceAllInterfaces()
                                   .WithServiceSelf()
                                   .LifestylePerSession());
        }

        private static void RegisterEventListeners(IWindsorContainer container)
        {
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
                                   .Where(l => l.IsEventListener())
                                   .WithServiceAllInterfaces()
                                   .Configure(x => x.Start())
                                   .LifestyleSingleton());
        }

        private static void RegisterConfiguration(IWindsorContainer container)
        {
            container.Register(Component
                .For<IPersistenceSettings>()
                .ImplementedBy<ApplicationSettings>());
        }

        private static void RegisterCommandHandlers(IWindsorContainer container)
        {
            // Register decorators
            container.Register(
                Component.For(typeof(ICommandHandler<>)).ImplementedBy(typeof(TransactionalCommandHandlerDecorator<>)),
                Component.For(typeof(ICommandHandler<>)).ImplementedBy(typeof(ConatinerCommandHandlerDecorator<>)));

            foreach (var assembly in GetAssemblies())
            {
                foreach (var registration in from f in assembly.GetTypes()
                                             where f.IsClass
                                             from i in f.GetInterfaces()
                                             where
                                                 i.IsGenericType &&
                                                 i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                                             let genericArgument = i.GetGenericArguments()[0]
                                             where !genericArgument.ContainsGenericParameters
                                             select new { Impl = f, Key = genericArgument.FullName })
                {
                    container.Register(Component.For<ICommandHandler>()
                                           .ImplementedBy(registration.Impl)
                                           .Named(registration.Key)
                                           .LifestyleTransient());
                }
            }
            container.Register(Component.For<ICommandHandlerFactory>()
                                   .AsFactory(f => f.SelectedWith(new CommandHandlerFactoryComponentSelector())));
        }

        private static void AddResolversAndFacilities(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CachingSessionResolver());
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<StartableFacility>();
        }

        private class CachingSessionResolver : CachingFactorySessionResolver
        {
            private static readonly object Locker = new object();

            // Cache sessions at the web-request level
            private static IDictionary SessionCache
            {
                get { return HttpContext.Current.Items; }
            }

            protected override ISession BuildSession(Assembly assembly)
            {
                lock (Locker)
                {
                    var assemblyName = assembly.GetName();
                    var cacheKey = assemblyName.Name;
                    ISession session;
                    if (SessionCache.Contains(cacheKey))
                    {
                        session = (ISession)SessionCache[cacheKey];
                    }
                    else
                    {
                        session = base.BuildSession(assembly);
                        SessionCache.Add(cacheKey, session);
                    }

                    return session;
                }
            }
        }

        private class CachingFactorySessionResolver : SessionResolver
        {
            // Consider using a thread safe collection (if it makes sense): http://msdn.microsoft.com/en-us/library/dd997305.aspx

            // Cache session factories at the applicaiton level
            private static readonly Dictionary<string, ISessionFactory> SessionFactoryCache =
                new Dictionary<string, ISessionFactory>();

            protected override ISessionFactory BuildSessionFactory(Assembly assembly)
            {
                lock (SessionFactoryCache)
                {
                    var assemblyName = assembly.GetName();
                    var cacheKey = assemblyName.Name;
                    ISessionFactory sessionFactory;
                    if (SessionFactoryCache.ContainsKey(cacheKey))
                    {
                        sessionFactory = SessionFactoryCache[cacheKey];
                    }
                    else
                    {
                        sessionFactory = base.BuildSessionFactory(assembly);
                        SessionFactoryCache.Add(cacheKey, sessionFactory);
                    }
                    return sessionFactory;
                }
            }
        }

        private class SessionResolver : ISubDependencyResolver
        {
            public bool CanResolve(
                CreationContext context,
                ISubDependencyResolver contextHandlerResolver,
                ComponentModel model,
                DependencyModel dependency)
            {
                var canResolve = dependency.TargetType == typeof(ISession);
                return canResolve;
            }

            public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
            {
                var implementation = model.Implementation;
                var assembly = implementation.Assembly;

                return Resolve(assembly);
            }

            private object Resolve(Assembly assembly)
            {
                var session = BuildSession(assembly);
                return session;
            }

            protected virtual ISession BuildSession(Assembly assembly)
            {
                var sessionFactory = BuildSessionFactory(assembly);
                var sessionBuilder = new NHibernateSessionBuilder(sessionFactory);
                var session = sessionBuilder.Build();
                return session;
            }

            protected virtual ISessionFactory BuildSessionFactory(Assembly assembly)
            {
                var configuration = BuildNHibernateConfiguration(assembly);
                var sessionFactoryBuilder = new NHibernateSessionFactoryBuilder(configuration);
                var sessionFactory = sessionFactoryBuilder.Build();
                return sessionFactory;
            }

            // Builds NHibernateConfiguration for the given assembly (bounded context)
            protected virtual Configuration BuildNHibernateConfiguration(Assembly assembly)
            {
                var configurationBuilder = new NHibernateConfigurationBuilder("db", assembly);
                return configurationBuilder.Build();
            }
        }
    }
}