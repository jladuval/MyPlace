namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Accounts.Interfaces.Commands.Profile;
    using Base.CQRS.Commands;
    using Core.Extensions;

    public class ProfileController : Controller
    {
        private readonly IGate _gate;

        public ProfileController(IGate gate)
        {
            _gate = gate;
        }

        public ActionResult Index(Guid? id)
        {
            return View(id == null ? "PrivateProfile" : "PublicProfile");
        }

        [HttpPost]
        [Authorize]
        public ActionResult SelectImage(string fileName)
        {
            _gate.Dispatch(new SelectProfileImageCommand(User.TryGetPrincipal().UserId, fileName));

            return Json("Success");
        }
    }
}
