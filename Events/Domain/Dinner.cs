namespace Events.Domain
{
    using System;

    using Base.DDD.Domain;

    public class Dinner : Entity
    {
        public User User { get; set; }

        public Location Location { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool Dry { get; set; }

        public string Description { get; set; }

        public DateTime? ClosedDate { get; set; }

        public DateTime Date { get; set; }

        public Dinner() { }

        public Dinner(
            User user, Location location, string starter, string main, string dessert, bool dry, string description, DateTime date)
        {
            User = user;
            Location = location;
            Starter = starter;
            Main = main;
            Dessert = dessert;
            Dry = dry;
            Description = description;
            Date = date;
        }

        public void Close()
        {
            ClosedDate = DateTime.UtcNow;
        }
    }
}
