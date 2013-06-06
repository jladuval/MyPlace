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

        public void LogIn(string email, bool rememberMe, Guid userId, IList<string> roles)
        {
            var info = new CustomPrincipalInfo
            {
                Email = email,
                UserId = userId,
                Roles = roles
            };
            var cookie = info.CreateAuthenticationCookie(DateTime.Now, Timeout, rememberMe);
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Session["IsLoggedIn"] = true;
        }
    }
}
