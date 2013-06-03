using Base.Infrastructure.Attributes;

namespace Base.DDD.Domain.Support
{
    [Component]
    public class InjectorHelper
    {
        public IDomainEventPublisher EventPublisher { get; set; }

        public void InjectDependencies(AggregateRoot aggregateRoot)
        {
            if (aggregateRoot != null)
            {
                aggregateRoot.EventPublisher = EventPublisher;
            }
        }
    }
}