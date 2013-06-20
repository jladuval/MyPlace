namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class DinnerListController : Controller
    {
        public ActionResult Index()
        {
            return View("DinnerList");
        }

        public ActionResult Host(Guid id)
        {
            return View("DinnerList");
        }
    }
}
