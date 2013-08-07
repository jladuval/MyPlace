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
            Mapper.CreateMap<ImageDto, ImageModel>();
            Mapper.CreateMap<DinnerListDto, DinnerListModel>();
            Mapper.CreateMap<DinnerListItemDto, DinnerListItemModel>();
            Mapper.CreateMap<DinnerDto, ViewDinnerModel>();
        }
    }
}