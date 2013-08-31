namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Accounts.Interfaces.Commands.Profile;
    using Accounts.Interfaces.Readers;
    using AutoMapper;
    using Base.CQRS.Commands;
    using Core.Extensions;
    using Models.Profile;

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
            id = id ?? User.TryGetPrincipal().UserId;
            return View("PublicProfile", Mapper.Map<PublicProfileModel>(_profileReader.GetPublicProfile(id.Value)));
        }

        public ActionResult Edit()
        {
            return View(
                "PrivateProfile",
                Mapper.Map<PrivateProfileModel>(_profileReader.GetPrivateProfile(User.TryGetPrincipal().UserId)));
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProfile(PrivateProfileModel model)
        {
            _gate.Dispatch(
                new MoreDetailsCommand(
                    User.TryGetPrincipal().UserId,
                    model.FirstName,
                    model.LastName,
                    model.Address,
                    model.Suburb,
                    model.City,
                    model.Country,
                    model.Postcode,
                    model.Gender,
                    model.Orientation,
                    model.Romance,
                    model.Friendship) 
                    {
                        Description = model.Description 
                    });
            return RedirectToAction("Index", new { id = User.TryGetPrincipal().UserId });
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
