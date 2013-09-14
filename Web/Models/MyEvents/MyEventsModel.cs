namespace Web.Models.MyEvents
{
    using System.Collections.Generic;

    public class MyEventsModel
    {
        public IList<MyEventsDinnerModel> AppliedDinners { get; set; }

        public IList<MyEventsDinnerModel> HostedDinners { get; set; }

        public IList<MyEventsDinnerModel> AttendedDinners { get; set; }
    }
}