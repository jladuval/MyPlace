namespace Common.DI
{
    using Castle.Core;
    using Castle.MicroKernel.Facilities;

    using Base.DDD.Infrastructure.Events;

    public class SubscribeEventListenerFacility : AbstractFacility
    {
        public IEventSubscriber EventSubscriber { get; private set; }

        protected override void Init()
        {
            Kernel.ComponentCreated += OnComponentCreated;
            Kernel.ComponentDestroyed += OnComponentDestroyed;
            
            EventSubscriber = Kernel.Resolve<IEventSubscriber>();
        }

        void OnComponentDestroyed(ComponentModel model, object instance)
        {
            if (instance is IEventListener)
                EventSubscriber.Unsubscribe((IEventListener)instance);
        }

        void OnComponentCreated(ComponentModel model, object instance)
        {
            if (instance is IEventListener)
                EventSubscriber.Subscribe((IEventListener)instance);
        }
    }
}