namespace Security.Domain
{
    using Base.DDD.Domain;

    public class Email : Entity
    {
        public string Payload { get; set; }

        public string Address { get; set; }

        public string TemplateName { get; set; }

        public int Priority { get; set; }
    }
}
