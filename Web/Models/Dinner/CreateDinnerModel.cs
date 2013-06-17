namespace Web.Models.Dinner
{
    using System;
    using System.Collections.Generic;

    public class CreateDinnerModel
    {
        public string Starter { get; set; }

        public Guid StarterAssignee { get; set; }

        public string Main { get; set; }

        public Guid MainAssignee { get; set; }

        public string Dessert { get; set; }

        public Guid DessertAssignee { get; set; }

        public string Drinks1 { get; set; }

        public Guid Drinks1Assignee { get; set; }

        public string Drinks2 { get; set; }

        public Guid Drinks2Assignee { get; set; }

        public Guid DriverAssignee { get; set; }

        public IList<ParticipantModel> Participants { get; set; }
    }

    public class ParticipantModel
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public bool Host { get; set; }
    }
}