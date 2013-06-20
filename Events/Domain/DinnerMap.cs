namespace Events.Domain
{
    using System;

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

            Map(x => x.Starter).Not.Nullable();

            Map(x => x.Main).Not.Nullable();

            Map(x => x.Dessert).Not.Nullable();

            Map(x => x.Description).Not.Nullable();

            Map(x => x.Dry).Not.Nullable();

            Map(x => x.Date).Not.Nullable();

            Map(x => x.ClosedDate).Nullable();
        }
    }
}
