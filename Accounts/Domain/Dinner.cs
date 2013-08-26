namespace Accounts.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
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

        public ICollection<DinnerApplicant> Applicants { get; set; }

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
            Applicants = new List<DinnerApplicant>();
        }

        public void Close()
        {
            ClosedDate = DateTime.UtcNow;
        }

        public void UserApplied(User user)
        {
            var application = new DinnerApplicant(user, this);
            Applicants.Add(application);
            user.AppliedDinners.Add(application);
        }
    }
}
