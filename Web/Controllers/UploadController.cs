using Accounts.Interfaces.Commands;
using Base.CQRS.Commands;
using Web.Core.Extensions;

namespace Web.Controllers
{
    using System.Web;
    using Base.AzureStorage;
    using System.Web.Mvc;

    public class UploadController : Controller
    {
        private readonly IBlobStorage _storage;

        private readonly IGate _gate;

        public UploadController(IBlobStorage storage, IGate gate)
        {
            _storage = storage;
            _gate = gate;
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfileImage(HttpPostedFileBase file)
        {
            var userId = User.TryGetPrincipal().UserId;

            var folderPath = userId + "/Profile";

            var url = _storage.SaveImage(file, userId + "/Profile");

            _gate.Dispatch(new AddProfileImageCommand(userId, url, folderPath, file.FileName));

            return Json(url);
        }

        [HttpPost]
        public ActionResult RemoveImage(string imagePath)
        {
            return Json("done");
        }
    }
}
