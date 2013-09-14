namespace Accounts.Interfaces.Presentation.Dinner
{
    using System;

    public class DinnerListItemDto
    {
        public Guid Id { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool DryDinner { get; set; }

        public bool Accepted { get; set; }

        public DateTime EventDate { get; set; }

        public double Distance { get; set; }
    }
}
