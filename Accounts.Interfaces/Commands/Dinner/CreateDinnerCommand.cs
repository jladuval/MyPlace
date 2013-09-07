namespace Accounts.Interfaces.Commands.Dinner
{
    using System;

    public class CreateDinnerCommand
    {
        public Guid UserId { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool Dry { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string PartnerEmail { get; set; }

        public string HostUrl { get; set; }
    }
}
