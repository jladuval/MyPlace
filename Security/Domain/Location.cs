namespace Security.Domain
{
    using Base.DDD.Domain;

    using NetTopologySuite.Geometries;

    public class Location : Entity
    {
        public string Address { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public Point GeoLoc { get; set; }
    }
}
