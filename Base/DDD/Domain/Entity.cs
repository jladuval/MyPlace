using System;
using Base.DDD.Domain.Annotations;

namespace Base.DDD.Domain
{
    [DomainEntity]
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; set; }

        public void MarkAsRemoved()
        {
            Deleted = true;
        }

        protected Entity()
        {
            CreatedDate = DateTime.UtcNow;
            Deleted = false;
        }
    }
}