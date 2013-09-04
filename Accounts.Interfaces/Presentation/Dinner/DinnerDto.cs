namespace Accounts.Interfaces.Presentation.Dinner
{
    using System;

    public class DinnerDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string ProfileImageUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? PartnerId { get; set; }

        public string PartnerImageUrl { get; set; }

        public string PartnerFirstName { get; set; }

        public string PartnerLastName { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool DryDinner { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public bool HasApplied { get; set; }
    }
}
