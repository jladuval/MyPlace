namespace Accounts.Interfaces.Presentation.Profile
{
    using System.Collections.Generic;
    using Common.Enums;

    public class PrivateProfileDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public string ProfileImage { get; set; }

        public IList<ImageDto> ProfileImageUrls { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

        public Orientation Orientation { get; set; }

        public bool Friendship { get; set; }

        public bool Romance { get; set; }
    }
}
