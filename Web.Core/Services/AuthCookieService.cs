namespace Web.Core.Services
{
    using System;
    using System.Web;
    using System.Web.Security;

    using Base.DDD.Domain.Annotations;

    using Web.Core;
    using Web.Core.Extensions;

    [DomainService]
    public class AuthCookieService : IAuthCookieService
    {
        public CustomPrincipalInfo GetUserCookiesInfo()
        {
            // TODO: Move the dependency to an external service or to the consumer
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return null;

            var ticket = cookie.GetFormsAuthenticationTicket();
            return ticket.GetCustomPrincipalInfo();
        }

        public void UpdateUserCookiesInfo(CustomPrincipalInfo info)
        {
            // TODO: Move the dependency to an external service or to the consumer
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = cookie.GetFormsAuthenticationTicket();
            var updated = ticket.RecreateAuthenticationCookie(info, DateTime.Now);
            HttpContext.Current.Response.Cookies.Add(updated);
        }
    }
}
