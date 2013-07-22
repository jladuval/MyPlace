namespace Accounts.Domain
{
    using System;

    using FluentNHibernate.Mapping;

    using Infrastructure.NHibernate.CustomTypes;

    public class LocationMap : ClassMap<Location>
    {
        public LocationMap()
        {
            Table("Locations");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            Map(x => x.CreatedDate).Not.Nullable();

            Map(x => x.ModifiedDate).Not.Nullable();

            Map(c => c.Address).Not.Nullable();

            Map(c => c.Suburb).Not.Nullable();

            Map(c => c.City).Not.Nullable();

            Map(c => c.Country).Not.Nullable();

            Map(c => c.Postcode).Not.Nullable();

            Map(c => c.Latitude).Not.Nullable();

            Map(c => c.Longitude).Not.Nullable();

            Map(x => x.GeoLoc)
                .CustomType(typeof(Wgs84GeographyType))
                .Not.Nullable()
                .CustomSqlType("GEOGRAPHY");
        }
    }
}
