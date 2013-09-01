namespace Accounts.Interfaces.Presentation.Dinner
{
    using System;

    public class DinnerDto
    {
        public Guid Id { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool DryDinner { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid UserId { get; set; }
    }
}
