namespace Web.Controllers
{
    using System.Web.Mvc;

    using Web.Models.Home;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new EntityModel());
        }

        [HttpPost]
        public ActionResult ChangeCurrentName(EntityModel model)
        {
            return View("Index", new EntityModel());
        }
    }
}
