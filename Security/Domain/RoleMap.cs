namespace Security.Domain
{
    using System;

    using FluentNHibernate.Mapping;

    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Roles");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.CreatedDate).Not.Nullable();
            Map(x => x.ModifiedDate).Not.Nullable();
            Map(x => x.Name).Not.Nullable().Unique();
            HasManyToMany(x => x.Users)
                .Table("UsersInRoles")
                .ParentKeyColumn("RoleId")
                .ChildKeyColumn("UserId");
        }
    }
}
