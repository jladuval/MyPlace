namespace Accounts.Readers
{
    using System;
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
                .Fetch(x => x.Location)
                .Where(x => x.Id == userId);
            var profileDto = 
                user.Select(x => new PrivateProfileDto
                {
                    Address = x.Location.Address,
                    City = x.Location.City,
                    Country = x.Location.Country,
                    Postcode = x.Location.Postcode,
                    Suburb = x.Location.Suburb,
                    Lat = x.Location.Latitude,
                    Long = x.Location.Longitude,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Description = x.Description,
                    Friendship = x.Friendship,
                    Orientation = x.Orientation,
                    Gender = x.Gender,
                    Romance = x.Romance,
                    ProfileImage = x.ProfileImageUrl
                }).First();
            profileDto.ProfileImageUrls =
                user.First().ProfileImages.Select(i => new ImageDto
                {
                    Url = i.Url,
                    FileName = i.ImageName
                }).ToList();
            return profileDto;
        }
    }
}
