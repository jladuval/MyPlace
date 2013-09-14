namespace Web.Models.MyEvents
{
    using System;

    public class MyEventsDinnerModel
    {
        public Guid DinnerId { get; set; }

        public string Host { get; set; }

        public string Partner { get; set; }

        public string Date { get; set; }

        public bool Pending { get; set; }

        public string AppliedPartner { get; set; }

        public Guid? ApprovalToken { get; set; }

        public int ApplicantCount { get; set; }
    }
}