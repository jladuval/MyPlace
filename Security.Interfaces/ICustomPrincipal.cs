using CQRS.Enums;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace CQRS.Security.Interfaces.Application
{
    public interface ICustomPrincipal : IPrincipal
    {
        Guid UserId { get; set; }

        string Email { get; set; }

        IEnumerable<UserRoles> Roles { get; set; }
    }
}
