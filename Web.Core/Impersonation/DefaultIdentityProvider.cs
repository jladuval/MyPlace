namespace Web.Core.Impersonation
{
    using System.Web;

    using Base.DDD.Domain.Annotations;

    using Web.Core;
    using Web.Core.Services;

    [DomainService]
    public class DefaultIdentityProvider : IIdentityProvider
    {
        private readonly IAuthCookieService _authCookieService;

        public DefaultIdentityProvider(IAuthCookieService authCookieService)
        {
            _authCookieService = authCookieService;
        }

        public void ProcessIdentity(HttpApplication application)
        {
            var authCookiesPayload = _authCookieService.GetUserCookiesInfo();
            if (authCookiesPayload != null)
            {
                ProcessAuthCookiesPayload(application, authCookiesPayload);
            }
        }

        protected virtual void ProcessAuthCookiesPayload(HttpApplication application, CustomPrincipalInfo principalInfo)
        {
            application.Context.User = (CustomPrincipal)principalInfo;
        }
    }
}
