namespace Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Accounts.Interfaces.Commands.Profile;

    using Base.AzureStorage;
    using Base.CQRS.Commands;

    using Web.Core.Extensions;

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

            var url = _storage.SaveImage(file, folderPath);

            if (url != null) _gate.Dispatch(new AddProfileImageCommand(userId, url, folderPath, file.FileName));

            return Json(url);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveProfileImage(string fileName)
        {
            var userId = User.TryGetPrincipal().UserId;

            var folderPath = userId + "/Profile";

            var url = _storage.DeleteImage(folderPath, fileName);

            if(url != null) _gate.Dispatch(new DeleteProfileImageCommand(userId, url, folderPath, fileName));

            return Json("done");
        }
    }
}
