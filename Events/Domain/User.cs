namespace Events.Domain
{
    using Base.DDD.Domain;

    public class User : AggregateRoot
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Location Location { get; set; }
    }
}
