namespace Accounts.Interfaces.Commands
{
    using System;

    public class MoreDetailsCommand
    {
        public Guid UserId { get; set; }

        public string Address { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public MoreDetailsCommand(
            Guid userId,
            string firstName,
            string lastName,
            string address,
            string suburb,
            string city,
            string country,
            string postcode,
            double latitude,
            double longitude)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Suburb = suburb;
            City = city;
            Country = country;
            Postcode = postcode;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
