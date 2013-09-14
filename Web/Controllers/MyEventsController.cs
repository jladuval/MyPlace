using System.Web.Mvc;

namespace Web.Controllers
{
    using Accounts.Interfaces.Presentation.Dinner;
    using Accounts.Interfaces.Readers;
    using Core.Extensions;

    [Authorize]
    public class MyEventsController : Controller
    {
        private readonly IDinnerReader _dinnerReader;

        public MyEventsController(IDinnerReader dinnerReader)
        {
            _dinnerReader = dinnerReader;
        }

        public ActionResult Index()
        {
            var dto = _dinnerReader.GetAppliedDinnerList(User.TryGetPrincipal().UserId);
            return View("MyEvents");
        }
    }
}
