using System.Collections.Generic;

namespace Accounts.Domain
{
    using System.Linq;
    using Base.DDD.Domain;
    using Common.Enums;
    using Orientation = Common.Enums.Orientation;

    public class User : AggregateRoot
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Location Location { get; set; }

        public ICollection<Image> ProfileImages { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Description { get; set; }

        public bool Romance { get; set; }

        public bool Friendship { get; set; }

        public Gender Gender { get; set; }

        public Orientation Orientation { get; set; }

        public virtual ICollection<Dinner> AppliedDinners { get; set; } 

        public User()
        {
            AppliedDinners = new List<Dinner>();
        }

        public void AddProfileImage(Image image)
        {
            var profileImage =
                ProfileImages.FirstOrDefault(x => x.ImageName == image.ImageName && x.FolderPath == x.FolderPath);
            if (profileImage == null)
                ProfileImages.Add(image);
            else
                profileImage.Url = image.Url;
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
