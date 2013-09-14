using System.Web.Mvc;

namespace Web.Controllers
{
    using System.Collections.Generic;
    using Accounts.Interfaces.Presentation.Dinner;
    using Accounts.Interfaces.Readers;
    using AutoMapper;
    using Core.Extensions;
    using Models.MyEvents;

    [Authorize]
    public class MyEventsController : Controller
    {
        private readonly IDinnerReader _dinnerReader;

        public MyEventsController(IDinnerReader dinnerReader)
        {
            _dinnerReader = dinnerReader;
        }

        public ActionResult Index()
        {
            var userId = User.TryGetPrincipal().UserId;
            return View("MyEvents", new MyEventsModel
            {
                AppliedDinners = Mapper.Map<List<MyEventsDinnerModel>>(_dinnerReader.GetAppliedDinnerList(userId)),
                AttendedDinners = Mapper.Map<List<MyEventsDinnerModel>>(_dinnerReader.GetAttendedDinnerList(userId)),
                HostedDinners = Mapper.Map<List<MyEventsDinnerModel>>(_dinnerReader.GetHostedDinnerList(userId))
            });
        }
    }
}
