namespace Web.Core
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;

    public interface ICustomPrincipal : IPrincipal
    {
        Guid UserId { get; set; }

        string Email { get; set; }

        IList<string> Roles { get; set; }

        bool HasDetails { get; set; }

        bool IsVerified { get; set; }
    }
}
