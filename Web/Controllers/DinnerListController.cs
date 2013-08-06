namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Accounts.Interfaces.Presentation.Dinner;
    using Accounts.Interfaces.Readers;
    using AutoMapper;
    using Core.Extensions;
    using Models.DinnerList;

    [Authorize]
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
            var skip = page == null ? 1 : page.Value * PageSize;
            var latlng = _profileReader.GetLatLong(User.TryGetPrincipal().UserId);
            var model = Mapper.Map<DinnerListDto, DinnerListModel>(_dinnerReader.GetDinnerList(latlng.Lat, latlng.Lng, skip , PageSize));
            model.Lat = latlng.Lat;
            model.Long = latlng.Lng;
            return View("DinnerList", model);
        }

        public ActionResult Host(Guid id)
        {
            return View("DinnerList");
        }
    }
}
