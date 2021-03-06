﻿namespace Accounts.Domain
{
    using System;
    using System.Collections.Generic;
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

        public virtual ICollection<DinnerApplicant> Applicants { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Guid? VerificationCode { get; set; }

        public virtual User Partner { get; set; }

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
            Applicants = Applicants ?? new List<DinnerApplicant>();
        }

        public void Close()
        {
            ClosedDate = DateTime.UtcNow;
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}
