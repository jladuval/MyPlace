namespace CQRS.Security.Interfaces.Application.Services
{
    public interface IAuthCookieService
    {
        CustomPrincipalInfo GetUserCookiesInfo();

        void UpdateUserCookiesInfo(CustomPrincipalInfo info);
    }
}
