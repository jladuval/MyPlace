namespace Accounts.Interfaces.Presentation.Dinner
{
    using System;

    public class DinnerApplicantDto
    {
        public Guid ApplicantId { get; set; }

        public Guid? PartnerId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string GenderOrientation { get; set; }

        public string PartnerName { get; set; }

        public string PartnerGenderOrientation { get; set; }
    }
}
