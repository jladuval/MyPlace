namespace Web.Core.Services
{
    using System;
    using System.Collections.Generic;

    public interface IAuthenticationService
    {
        void LogOff();

        void LogIn(string email, bool rememberMe, Guid userId, IList<string> roles);
    }
}
