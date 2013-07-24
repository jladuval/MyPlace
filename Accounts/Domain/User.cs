using System.Collections.Generic;

namespace Accounts.Domain
{
    using System.Linq;
    using Base.DDD.Domain;

    public class User : AggregateRoot
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Location Location { get; set; }

        public ICollection<Image> ProfileImages { get; set; }

        public string ProfileImageUrl { get; set; }

        public void AddProfileImage(Image image)
        {
            ProfileImages.Add(image);
        }

        public void SetProfileImage(string imageName)
        {
            var profileImage = ProfileImages.FirstOrDefault(x => x.ImageName == imageName);

            if (profileImage != null)
            {
                ProfileImageUrl = profileImage.Url;
            }
        }
    }
}
