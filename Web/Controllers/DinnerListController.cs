namespace Web.Controllers
{
    using System.Web.Mvc;

    public class DinnerListController : Controller
    {
        public ActionResult Index()
        {
            return View("DinnerList");
        }
    }
}
