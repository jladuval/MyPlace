namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Accounts.Interfaces.Readers;
    using Core.Extensions;

    public class DinnerListController : Controller
    {
        private readonly IDinnerReader _dinnerReader;
        private readonly IProfileReader _profileReader;

        private const int PageSize = 20;

        public DinnerListController(IDinnerReader dinnerReader, IProfileReader profileReader)
        {
            _dinnerReader = dinnerReader;
            _profileReader = profileReader;
        }

        public ActionResult Index(int? page)
        {
            var skip = page == null ? 0 : page*PageSize;
            var latlng = _profileReader.GetLatLong(User.TryGetPrincipal().UserId);
            _dinnerReader.GetDinnerList(-122.34900, 47.65100, skip , PageSize);
            return View("DinnerList");
        }

        public ActionResult Host(Guid id)
        {
            return View("DinnerList");
        }
    }
}
