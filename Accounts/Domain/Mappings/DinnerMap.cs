namespace Accounts.Domain.Mappings
{
    using System;
    using Domain;
    using FluentNHibernate.Mapping;

    public class DinnerMap : ClassMap<Dinner>
    {
        public DinnerMap()
        {
            Table("Dinners");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            Map(x => x.CreatedDate).Not.Nullable();

            Map(x => x.ModifiedDate).Not.Nullable();

            References(x => x.User).Column("UserId").Not.Nullable().Cascade.All();

            References(x => x.Location).Column("LocationId").Not.Nullable().Cascade.All();

            References(x => x.Partner).Column("PartnerId").Nullable().Cascade.SaveUpdate();

            Map(x => x.VerificationCode).Nullable();

            Map(x => x.Starter).Not.Nullable();

            Map(x => x.Main).Not.Nullable();

            Map(x => x.Dessert).Not.Nullable();

            Map(x => x.Description).Not.Nullable();

            Map(x => x.Dry).Not.Nullable();

            Map(x => x.Date).Not.Nullable();

            Map(x => x.ClosedDate).Nullable();

            HasMany(x => x.Applicants)
                .KeyColumn("DinnerId")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Comments)
                .KeyColumn("DinnerId")
                .Cascade.All();
        }
    }
}
