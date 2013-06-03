namespace Security.Domain
{
    using System;

    using FluentNHibernate.Mapping;

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.CreatedDate).Not.Nullable();
            Map(x => x.ModifiedDate).Not.Nullable();
            Map(x => x.FirstName).Nullable();
            Map(x => x.LastName).Nullable();
            Map(x => x.Email).Not.Nullable().Unique();
            Map(x => x.Password).Nullable();
            Map(x => x.Salt).Nullable();
            Map(x => x.IsVerified).Nullable();
            Map(x => x.VerificationCode);
            HasManyToMany(x => x.Roles)
                .Table("UsersInRoles")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId")
                .Not.LazyLoad();
        }
    }
}
