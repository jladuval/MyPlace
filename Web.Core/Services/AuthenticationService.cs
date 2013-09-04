namespace Web.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;

    using Base.DDD.Domain.Annotations;

    using Web.Core.Extensions;

    [DomainService]
    public class AuthenticationService : IAuthenticationService
    {
        private const int Timeout = 30;

        public void LogOff()
        {
            HttpContext.Current.Session["IsLoggedIn"] = false;
            FormsAuthentication.SignOut();
        }

        public void LogIn(string email, bool rememberMe, Guid userId, IList<string> roles, bool hasDetails)
        {
            var info = new CustomPrincipalInfo
            {
                Email = email,
                UserId = userId,
                Roles = roles,
                HasDetails = hasDetails
            };
            var cookie = info.CreateAuthenticationCookie(DateTime.Now, Timeout, rememberMe);
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Session["IsLoggedIn"] = true;
        }

        public void AddedDetails(ICustomPrincipal principal)
        {
            var info = new CustomPrincipalInfo
            {
                Email = principal.Email,
                UserId = principal.UserId,
                Roles = principal.Roles,
                HasDetails = true
            };
            var cookie = info.CreateAuthenticationCookie(DateTime.Now, Timeout, true);
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Session["IsLoggedIn"] = true;
        }
    }
}
