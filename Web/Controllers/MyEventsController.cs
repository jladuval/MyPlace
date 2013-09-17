using System.Web.Mvc;

namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using Accounts.Interfaces.Readers;
    using AutoMapper;
    using Base.CQRS.Commands;
    using Core.Extensions;
    using Models.MyEvents;

    [Authorize]
    public class MyEventsController : Controller
    {
        private readonly IDinnerReader _dinnerReader;
        private readonly IGate _gate;

        public MyEventsController(IDinnerReader dinnerReader, IGate gate)
        {
            _dinnerReader = dinnerReader;
            _gate = gate;
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

        public ActionResult Review(Guid id)
        {
            if (_dinnerReader.UserIsOwner(User.TryGetPrincipal().UserId, id))
            {
                var model = Mapper.Map<ReviewModel>(_dinnerReader.GetDinnerForReview(id));
                return View("ReviewApplicants", model);
            }
            TempData["ErrorMessage"] = "You need to have hosted this dinner to review it";
            return RedirectToAction("Index");
        }
    }
}
