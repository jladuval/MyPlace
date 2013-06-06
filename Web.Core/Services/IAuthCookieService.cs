namespace Web.Core.Services
{
    using Web.Core;

    public interface IAuthCookieService
    {
        CustomPrincipalInfo GetUserCookiesInfo();

        void UpdateUserCookiesInfo(CustomPrincipalInfo info);
    }
}
