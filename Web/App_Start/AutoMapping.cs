namespace Web.App_Start
{
    using Accounts.Interfaces.Presentation;
    using Accounts.Interfaces.Presentation.Dinner;
    using Accounts.Interfaces.Presentation.Profile;
    using AutoMapper;
    using Models.Dinner;
    using Models.DinnerList;
    using Models.Profile;
    using Models.Shared;

    public static class AutoMapping
    {
        public static void CreateMappings()
        {
            Mapper.CreateMap<PrivateProfileDto, PrivateProfileModel>();
            Mapper.CreateMap<PublicProfileDto, PublicProfileModel>();
            Mapper.CreateMap<ImageDto, ImageModel>();
            Mapper.CreateMap<DinnerListDto, DinnerListModel>();
            Mapper.CreateMap<DinnerListItemDto, DinnerListItemModel>()
                .ForMember(x => x.Date, opt => opt.MapFrom(x => x.EventDate));
            Mapper.CreateMap<DinnerDto, ViewDinnerModel>();
        }
    }
}