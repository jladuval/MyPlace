namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Accounts.Interfaces.Commands.Profile;
    using Accounts.Interfaces.Readers;
    using Base.CQRS.Commands;
    using Core.Extensions;

    public class ProfileController : Controller
    {
        private readonly IGate _gate;
        private readonly IProfileReader _profileReader;

        public ProfileController(IGate gate, IProfileReader profileReader)
        {
            _gate = gate;
            _profileReader = profileReader;
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

            return Json(_profileReader.GetImageUrl(User.TryGetPrincipal().UserId, fileName));
        }
    }
}
