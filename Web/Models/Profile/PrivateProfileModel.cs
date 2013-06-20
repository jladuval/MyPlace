namespace Web.Models.Profile
{
    using System.Collections.Generic;

    using Common.Enums;

    public class PrivateProfileModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public IList<string> ProfileImageUrls { get; set; }

        public IList<string> LeastFavouriteFoods { get; set; }

        public IList<string> PersonalityFood { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

        public Orientation Orientation { get; set; }
    }
}