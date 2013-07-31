namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Accounts.Interfaces.Readers;

    public class DinnerListController : Controller
    {
        private readonly IDinnerReader _dinnerReader;

        public DinnerListController(IDinnerReader dinnerReader)
        {
            _dinnerReader = dinnerReader;
        }

        public ActionResult Index()
        {
            _dinnerReader.GetDinnerList(-122.34900, 47.65100, 0 , 10);
            return View("DinnerList");
        }

        public ActionResult Host(Guid id)
        {
            return View("DinnerList");
        }
    }
}
