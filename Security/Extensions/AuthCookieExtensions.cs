namespace Security.Extensions
{
    using System;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.Security;

    using Security.Interfaces.Application;

    public static class AuthCookieExtensions
    {
        public static HttpCookie CreateAuthenticationCookie(this CustomPrincipalInfo info, DateTime current, int timeout, bool remember)
        {
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(info);
            var ticket = new FormsAuthenticationTicket(
                1, info.Email, current, current.AddMinutes(timeout), remember, userData);

            var secureTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, secureTicket) { Domain = FormsAuthentication.CookieDomain };
            return cookie;
        }

        public static FormsAuthenticationTicket GetFormsAuthenticationTicket(this HttpCookie cookie)
        {
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            return ticket;
        }

        public static CustomPrincipalInfo GetCustomPrincipalInfo(this FormsAuthenticationTicket ticket)
        {
            var serializer = new JavaScriptSerializer();
            var info = serializer.Deserialize<CustomPrincipalInfo>(ticket.UserData);
            return info;
        }

        public static HttpCookie RecreateAuthenticationCookie(this FormsAuthenticationTicket existing, DateTime current)
        {
            var info = existing.GetCustomPrincipalInfo();
            var timeout = existing.Expiration - existing.IssueDate;
            return CreateAuthenticationCookie(info, current, timeout.Minutes, existing.IsPersistent);
        }

        public static HttpCookie RecreateAuthenticationCookie(this FormsAuthenticationTicket existing, CustomPrincipalInfo newInfo, DateTime current)
        {
            var info = existing.GetCustomPrincipalInfo();
            info.Update(newInfo);
            var timeout = existing.Expiration - existing.IssueDate;
            return CreateAuthenticationCookie(info, current, timeout.Minutes, existing.IsPersistent);
        }

        public static void Update(this CustomPrincipalInfo current, CustomPrincipalInfo newInfo)
        {
            current.UserId = newInfo.UserId;
            current.Email = newInfo.Email;
        }
    }
}
