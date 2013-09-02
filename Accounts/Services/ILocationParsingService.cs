namespace Accounts.Services
{
    using GoogleMaps.LocationServices;

    public interface ILocationParsingService
    {
        MapPoint GetLatLong(string address, string suburb, string city, string country, string postcode);
    }
}
