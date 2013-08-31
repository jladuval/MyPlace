namespace Web.Models.Profile
{
    using System.Collections.Generic;

    using Common.Enums;

    using Web.Models.Shared;

    public class PublicProfileModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public IList<ImageModel> ProfileImageUrls { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

        public Orientation Orientation { get; set; }

        public bool Friendship { get; set; }

        public bool Romance { get; set; }
    }
}