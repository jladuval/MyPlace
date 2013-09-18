namespace Web.Models.MyEvents
{
    using System;

    public class ReviewApplicantModel
    {
        public Guid ApplicationId { get; set; }

        public Guid? PartnerId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string GenderOrientation { get; set; }

        public string PartnerName { get; set; }

        public string PartnerGenderOrientation { get; set; }
    }
}