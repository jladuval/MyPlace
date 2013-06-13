namespace Accounts.Domain
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

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Point GeoLoc { get; set; }

        public Location() { }

        public Location(
            string address, string suburb, string city, string country, string postcode, double latitude, double longitude) : base()
        {
            Address = address;
            Suburb = suburb;
            City = city;
            Country = country;
            Postcode = postcode;
            Latitude = latitude;
            Longitude = longitude;
            GeoLoc = new Point(longitude, latitude);
        }
    }
}
