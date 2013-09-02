namespace Accounts.Services
{
    using System.Text;
    using Base.DDD.Domain.Annotations;
    using GoogleMaps.LocationServices;

    [DomainService]
    public class LocationParsingService : ILocationParsingService
    {
        public MapPoint GetLatLong(string address, string suburb, string city, string country, string postcode)
        {
            var locationString = LocationToString(address, suburb, city, country, postcode);
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(locationString);
            return point;
        }

        private string LocationToString(string address, string suburb, string city, string country, string postcode)
        {
            var result = new StringBuilder();
            result.Append(address);
            result.Append(", ");
            result.Append(suburb);
            result.Append(", ");
            result.Append(city);
            result.Append(", ");
            result.Append(country);
            result.Append(", ");
            result.Append(postcode);
            return result.ToString();
        }
    }
}
