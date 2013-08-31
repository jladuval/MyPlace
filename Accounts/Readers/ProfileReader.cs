namespace Accounts.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base.CQRS.Query.Attributes;
    using Domain;
    using Interfaces.Presentation;
    using Interfaces.Presentation.Profile;
    using Interfaces.Readers;
    using NHibernate;
    using NHibernate.Linq;

    [Reader]
    public class ProfileReader : IProfileReader
    {
        private readonly ISession _session;

        public ProfileReader(ISession session)
        {
            _session = session;
        }

        public string GetImageUrl(Guid userId, string fileName)
        {
            return
                _session.Query<User>()
                    .Fetch(x => x.ProfileImages)
                    .Single(x => x.Id == userId)
                    .ProfileImages.Single(x => x.ImageName == fileName)
                    .Url;
        }

        public PrivateProfileDto GetPrivateProfile(Guid userId)
        {
            var user = _session.Query<User>()
                               .Fetch(x => x.ProfileImages)
                               .Fetch(x => x.Location).Single(x => x.Id == userId);
            var profileDto = 
                new PrivateProfileDto
                {
                    Address = user.Location.Address,
                    City = user.Location.City,
                    Country = user.Location.Country,
                    Postcode = user.Location.Postcode,
                    Suburb = user.Location.Suburb,
                    Lat = user.Location.Latitude,
                    Long = user.Location.Longitude,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    Friendship = user.Friendship,
                    Orientation = user.Orientation,
                    Gender = user.Gender,
                    Romance = user.Romance,
                    ProfileImage = user.ProfileImageUrl
                };
            profileDto.ProfileImageUrls = 
                user.ProfileImages.Select(i => new ImageDto
                {
                    Url = i.Url,
                    FileName = i.ImageName
                }).ToList();
            return profileDto;
        }

        public LatLngDto GetLatLong(Guid userId)
        {
            var user = _session.Query<User>()
                .Fetch(x => x.Location)
                .Single(x => x.Id == userId);
            return new LatLngDto
            {
                Lat = user.Location.Latitude,
                Lng = user.Location.Longitude
            };
        }

        public string GetLocationString(Guid userId)
        {
            var location = _session.Query<User>().Fetch(x => x.Location).Single(x => x.Id == userId).Location;
            return string.Format(
                "{0}, {1}, {2}, {3}", location.Address, location.Suburb, location.City, location.Postcode);
        }

        public PublicProfileDto GetPublicProfile(Guid id)
        {
            var user = _session.Query<User>()
                              .Fetch(x => x.ProfileImages)
                              .Fetch(x => x.Location).Single(x => x.Id == id);
            var profileDto =
                new PublicProfileDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    Friendship = user.Friendship,
                    Orientation = user.Orientation,
                    Gender = user.Gender,
                    Romance = user.Romance,
                    ProfileImage = user.ProfileImageUrl
                };
            profileDto.ProfileImageUrls =
                user.ProfileImages.Select(i => new ImageDto
                {
                    Url = i.Url,
                    FileName = i.ImageName
                }).ToList();
            return profileDto;
        }
    }
}
