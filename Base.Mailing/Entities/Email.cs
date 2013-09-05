namespace Base.Mailing.Entities
{
    using System;

    public class Email
    {
        public int Id { get; set; }

        public string TemplateName { get; set; }

        public string Payload { get; set; }

        public string Address { get; set; }

        public DateTime? SentDate { get; set; }
    }
}
