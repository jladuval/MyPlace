using System.Collections.Generic;

namespace Accounts.Domain
{
    using Base.DDD.Domain;

    public class User : AggregateRoot
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Location Location { get; set; }

        public ICollection<Image> ProfileImages { get; set; }

        public Image ProfileImage { get; set; }
    }
}
