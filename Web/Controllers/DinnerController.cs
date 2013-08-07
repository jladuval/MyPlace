namespace Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using Accounts.Interfaces.Commands;
    using Accounts.Interfaces.Commands.Dinner;
    using Accounts.Interfaces.Readers;
    using AutoMapper;
    using Base.CQRS.Commands;
    using Core.Extensions;
    using Models.Dinner;

    public class DinnerController : Controller
    {
        private readonly IGate _gate;
        private readonly IDinnerReader _dinnerReader;

        public DinnerController(IGate gate, IDinnerReader dinnerReader)
        {
            _gate = gate;
            _dinnerReader = dinnerReader;
        }

        public ActionResult Index(Guid id)
        {
            var model = Mapper.Map<ViewDinnerModel>(_dinnerReader.GetDinner(id));
            return View("ViewDinner", model);
        }

        public ActionResult Apply(Guid id)
        {
            _gate.Dispatch(new ApplyForDinnerCommand(User.TryGetPrincipal().UserId, id));
            var model = Mapper.Map<ViewDinnerModel>(_dinnerReader.GetDinner(id));
            return View("ViewDinner", model);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View("Create", new CreateDinnerModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateDinnerModel model)
        {
            DateTime date;
            if (!DateTime.TryParseExact(model.Date, "MM/dd/yyyy HH:mm", null, DateTimeStyles.None, out date))
                ModelState.AddModelError("Date", "The date entered was invalid");

            if (ModelState.IsValid)
            {
                _gate.Dispatch(
                    new CreateDinnerCommand
                        {
                            UserId = User.TryGetPrincipal().UserId,
                            Starter = model.Starter,
                            Main = model.Main,
                            Dessert = model.Dessert,
                            Dry = model.DryDinner,
                            Description = model.Description,
                            Date = date
                        });
                return RedirectToAction("Host", "DinnerList", new { Id = User.TryGetPrincipal().UserId });
            }
            return View("Create", model);
        }

        public ActionResult View(Guid id)
        {
            return View("Create", new CreateDinnerModel());
        }
    }
}
