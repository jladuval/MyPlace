﻿namespace Accounts.Interfaces.Presentation.Profile
{
    using System.Collections.Generic;

    using Common.Enums;

    public class PublicProfileDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public IList<ImageDto> ProfileImageUrls { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

        public Orientation Orientation { get; set; }

        public string Age { get; set; }

        public bool Friendship { get; set; }

        public bool Romance { get; set; }
    }
}
