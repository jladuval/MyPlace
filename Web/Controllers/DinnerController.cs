namespace Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using Base.CQRS.Commands;

    using Events.Interfaces.Commands;

    using Core.Extensions;
    using Models.Dinner;

    public class DinnerController : Controller
    {
        private readonly IGate _gate;

        public DinnerController(IGate gate)
        {
            _gate = gate;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Create", new CreateDinnerModel());
        }

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
