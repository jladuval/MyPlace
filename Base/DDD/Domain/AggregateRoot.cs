using System;
using Base.DDD.Domain.Annotations;

namespace Base.DDD.Domain
{
    [DomainAggregateRoot]
    public abstract class AggregateRoot : Entity
    {
        private IDomainEventPublisher _eventPublisher;
        protected internal virtual IDomainEventPublisher EventPublisher
        {
            get { return _eventPublisher; }
            set
            {
                if (_eventPublisher != null)
                    throw new InvalidOperationException("Publisher is already set. Probably You have logical error in code");
                _eventPublisher = value;
            }
        }
    }
}
