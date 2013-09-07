﻿namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Accounts.Interfaces.Commands;
    using Accounts.Interfaces.Commands.Profile;
    using Base.CQRS.Commands;
    using Mailing.Interfaces;
    using Security.Interfaces.Commands;
    using Security.Interfaces.Queries;

    using Core.Extensions;
    using Core.Services;
    using Models.Membership;

    public class MembershipController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;

        private readonly IAuthenticationService _authenticationService;

        public MembershipController(ISecurityUserReader securityUserReader, IGate gate, IAuthenticationService authenticationService)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
            _authenticationService = authenticationService;
        }        

        [Authorize]
        public ActionResult LogOff()
        {
            _authenticationService.LogOff();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(string returnUrl = null)
        {
            return View("Login");
        }

        public ActionResult Activate(string token)
        {
            var userdto = _securityUserReader.GetUserFromToken(token);
            if (userdto != null)
            {
                _gate.Dispatch(new ActivateUserCommand(token));
                _authenticationService.LogIn(userdto.Email, true, userdto.UserId, userdto.Roles, userdto.HasDetails);
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;
                var pass = model.Password;
                var rememberMe = model.RememberMe;

                var user = _securityUserReader.CheckUserCredentials(new CheckUserCredentialsQuery { Email = email, Password = pass });
                if (user != null)
                {
                    _authenticationService.LogIn(email, rememberMe, user.UserId, user.Roles, user.HasDetails);
                    return RedirectToAction("MoreDetails");
                }
            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(SignupModel model)
        {
            if (_securityUserReader.UserExists(model.Email))
                ModelState.AddModelError("Email", "This Email Address is in use");

            if (ModelState.IsValid)
            {
                var email = model.Email;
                var password = model.Password;

                _gate.Dispatch(new SignUpUserCommand(email, password)
                {
                    HostPath = Request.Url.Host
                });
                var user = _securityUserReader.CheckUserCredentials(new CheckUserCredentialsQuery { Email = email, Password = password });
                _authenticationService.LogIn(email, true, user.UserId, user.Roles, false);
                _gate.Dispatch(new RunMailerCommand());
                return RedirectToAction("MoreDetails");
            }
            return View("SignUp", model);
        }

        [Authorize]
        public ActionResult MoreDetails()
        {
            return View("MoreDetails");
        }

        [Authorize]
        [HttpPost]
        public ActionResult MoreDetails(MoreDetailsModel model)
        {
            _authenticationService.AddedDetails(User.TryGetPrincipal());
            _gate.Dispatch(new MoreDetailsCommand(
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
                    model.Friendship));
            return RedirectToAction("Index", "DinnerList");
        }
    }
}
