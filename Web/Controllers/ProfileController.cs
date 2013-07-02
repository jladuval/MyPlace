namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ProfileController : Controller
    {
        public ActionResult Index(Guid? id)
        {
            return View(id == null ? "PrivateProfile" : "PublicProfile");
        }
    }
}
