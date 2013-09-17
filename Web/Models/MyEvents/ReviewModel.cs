namespace Web.Models.MyEvents
{
    using System;
    using System.Collections.Generic;

    public class ReviewModel
    {
        public Guid Id { get; set; }

        public string Date { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public ICollection<ReviewApplicantModel> Applicants { get; set; }
    }
}