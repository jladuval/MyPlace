namespace Base.DDD.Domain
{
    using System;

    using FluentNHibernate.Mapping;

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
