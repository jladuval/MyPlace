namespace Accounts.Domain
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

            Map(x => x.ProfileImageUrl).Nullable();

            HasMany(x => x.ProfileImages)
                .KeyColumn("UserProfileId")
                .Cascade.All()
                .Inverse();

            References(x => x.Location).Column("LocationId").Nullable().Cascade.All();
        }
    }
}
