﻿namespace Infrastructure.NHibernate.CustomTypes
{
    using global::NHibernate.Spatial.Type;

    public class Wgs84GeographyType : MsSql2008GeographyType
    {
        protected override void SetDefaultSRID(GeoAPI.Geometries.IGeometry geometry)
        {
            geometry.SRID = 4326;
        }
    }
}
