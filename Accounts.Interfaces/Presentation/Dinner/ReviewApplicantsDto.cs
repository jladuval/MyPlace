namespace Accounts.Interfaces.Presentation.Dinner
{
    using System;
    using System.Collections.Generic;

    public class ReviewApplicantsDto
    {
        public Guid Id { get; set; }

        public string Date { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public ICollection<DinnerApplicantDto> Applicants { get; set; }
    }
}
