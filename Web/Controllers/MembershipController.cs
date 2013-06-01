namespace Web.Controllers
{
    using System.Web.Mvc;

    using WebCore.Security.Interfaces;

    public class MembershipController : Controller
    {
        private readonly IAccountService _accounts;

        public MembershipController(IAccountService accounts)
        {
            _accounts = accounts;
        }

        public ActionResult Index()
        {
            _accounts.LogoutUser();
            return View("SignUp");
        }
    }
}
