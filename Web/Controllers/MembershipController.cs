﻿namespace Web.Controllers
{
    using System.Web.Mvc;

    using Base.CQRS.Commands;

    using Security.Interfaces.Commands;
    using Security.Interfaces.Queries;

    using Web.Core.Services;
    using Web.Models.Membership;

    public class MembershipController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;

        private readonly IAuthenticationService _authenticationService;

        public MembershipController(ISecurityUserReader securityUserReader, IGate gate, IAuthenticationService authenticationService)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
            authenticationService = authenticationService;
        }        

        [Authorize]
        public ActionResult LogOff()
        {
            _authenticationService.LogOff();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View("Login");
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
                    _authenticationService.LogIn(email, rememberMe, user.UserId, user.Roles);
                    return RedirectToAction("Index", "Home", null);
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
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var email = model.Email;
                    var password = model.Password;

                    _gate.Dispatch(new SignUpUserCommand(email, password));
                    var user = _securityUserReader.CheckUserCredentials(new CheckUserCredentialsQuery { Email = email, Password = password });
                    _authenticationService.LogIn(email, true, user.UserId, user.Roles);
                    return RedirectToAction("Index", "Home", null);
                }
                return RedirectToAction("MoreDetails");
            }
            return View("SignUp", model);
        }

        [Authorize]
        public ActionResult MoreDetails()
        {
            return View("MoreDetails");
        }
    }
}
