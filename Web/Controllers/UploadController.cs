namespace Web.Controllers
{
    using System.Web;
    using Base.AzureStorage;
    using System.Web.Mvc;

    public class UploadController : Controller
    {
        private readonly IBlobStorage _storage;

        public UploadController(IBlobStorage storage)
        {
            _storage = storage;
        }
        

        [HttpPost]
        public ActionResult Image(HttpPostedFileBase file)
        {
            var url = _storage.SaveImage(file);

            return Json(url);
        }

        [HttpPost]
        public ActionResult RemoveImage(string imagePath)
        {
            return Json("done");
        }
    }
}
