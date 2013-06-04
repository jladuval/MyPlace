namespace Web.Controllers
{
    using System.Web.Mvc;

    using Base.CQRS.Commands;

    using Security.Interfaces.Commands;
    using Security.Interfaces.Queries;

    using Web.Models.Membership;

    public class MembershipController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;

        public MembershipController(ISecurityUserReader securityUserReader, IGate gate)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
        }        

        [Authorize]
        public ActionResult LogOff()
        {
            _gate.Dispatch(new LogOffUserCommand());
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
                    _gate.Dispatch(new LogInUserCommand { Email = email, UserId = user.UserId, RememberMe = rememberMe, Roles = user.Roles });
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
                    _gate.Dispatch(new LogInUserCommand { Email = email, UserId = user.UserId, RememberMe = true, Roles = user.Roles });
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
