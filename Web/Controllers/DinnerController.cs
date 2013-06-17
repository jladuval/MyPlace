namespace Web.Controllers
{
    using System.Web.Mvc;

    using Web.Models.Dinner;

    public class DinnerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Create", new CreateDinnerModel());
        }
    }
}
