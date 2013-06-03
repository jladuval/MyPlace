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
            this.Kernel.ComponentCreated += this.OnComponentCreated;
            this.Kernel.ComponentDestroyed += this.OnComponentDestroyed;
            
            this.EventSubscriber = this.Kernel.Resolve<IEventSubscriber>();
        }

        void OnComponentDestroyed(ComponentModel model, object instance)
        {
            if (instance is IEventListener)
                this.EventSubscriber.Unsubscribe((IEventListener)instance);
        }

        void OnComponentCreated(ComponentModel model, object instance)
        {
            if (instance is IEventListener)
                this.EventSubscriber.Subscribe((IEventListener)instance);
        }
    }
}