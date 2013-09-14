namespace Web.App_Start
{
    using Accounts.Interfaces.Presentation;
    using Accounts.Interfaces.Presentation.Comments;
    using Accounts.Interfaces.Presentation.Dinner;
    using Accounts.Interfaces.Presentation.Profile;
    using AutoMapper;
    using Models.Dinner;
    using Models.DinnerList;
    using Models.MyEvents;
    using Models.Profile;
    using Models.Shared;

    public static class AutoMapping
    {
        private const string Placeholder = "/Content/images/placeholder.jpg";

        public static void CreateMappings()
        {
            Mapper.CreateMap<PrivateProfileDto, PrivateProfileModel>();
            Mapper.CreateMap<PublicProfileDto, PublicProfileModel>();
            Mapper.CreateMap<PersonalDinnerListItem, MyEventsDinnerModel>();
            Mapper.CreateMap<ImageDto, ImageModel>();
            Mapper.CreateMap<DinnerListDto, DinnerListModel>();
            Mapper.CreateMap<DinnerListItemDto, DinnerListItemModel>()
                .ForMember(x => x.Date, opt => opt.MapFrom(x => x.EventDate))
                .AfterMap((src, dest) => { if (dest.ProfileImageUrl == null) dest.ProfileImageUrl = Placeholder; });
            Mapper.CreateMap<DinnerDto, ViewDinnerModel>()
                .AfterMap((src, dest) => { if (dest.ProfileImageUrl == null) dest.ProfileImageUrl = Placeholder; })
                .AfterMap((src, dest) => { if (dest.PartnerImageUrl == null) dest.PartnerImageUrl = Placeholder; });
            Mapper.CreateMap<CommentDto, CommentModel>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate.ToShortDateString()));
        }
    }
}