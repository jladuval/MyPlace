namespace Web.Models.Dinner
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ViewDinnerModel
    {
        public Guid Id { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool DryDinner { get; set; }

        public DateTime EventDate { get; set; }

        public double Distance { get; set; }

        public string Description { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid UserId { get; set; }

        public bool HasApplied { get; set; }

        [EmailAddress]
        public string PartnerEmail { get; set; }
    }
}