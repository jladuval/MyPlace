using System;
using FluentNHibernate.Mapping;

namespace Base.DDD.Domain
{
    public class EntityMap : ClassMap<Entity>
    {
        public EntityMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Deleted);
            Map(x => x.CreatedDate);
            Map(x => x.ModifiedDate);
            UseUnionSubclassForInheritanceMapping(); 
        }
    }
}
