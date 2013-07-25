namespace Web.App_Start
{
    using Accounts.Interfaces.Presentation;
    using Accounts.Interfaces.Presentation.Profile;
    using AutoMapper;
    using Models.Profile;
    using Models.Shared;

    public static class AutoMapping
    {
        public static void CreateMappings()
        {
            Mapper.CreateMap<PrivateProfileDto, PrivateProfileModel>();
            Mapper.CreateMap<ImageDto, ImageModel>();
        }
    }
}