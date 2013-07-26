namespace Accounts.Interfaces.Commands.Profile
{
    using System;
    using System.Text;

    using Common.Enums;

    public class MoreDetailsCommand
    {
        public Guid UserId { get; set; }

        public string Address { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

        public Orientation Orientation { get; set; }

        public bool Romance { get; set; }

        public bool Friendship { get; set; }

        public MoreDetailsCommand(
            Guid userId,
            string firstName,
            string lastName,
            string address,
            string suburb,
            string city,
            string country,
            string postcode,
            Gender gender,
            Orientation orientation,
            bool romance,
            bool friendship)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Suburb = suburb;
            City = city;
            Country = country;
            Postcode = postcode;
            Gender = gender;
            Orientation = orientation;
            Romance = romance;
            Friendship = friendship;
        }

        public string LocationToString()
        {
            var result = new StringBuilder();
            result.Append(Address);
            result.Append(", ");
            result.Append(Suburb);
            result.Append(", ");
            result.Append(City);
            result.Append(", ");
            result.Append(Country);
            result.Append(", ");
            result.Append(Postcode);
            return result.ToString();
        }
    }
}
