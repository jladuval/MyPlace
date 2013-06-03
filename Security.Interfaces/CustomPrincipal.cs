using CQRS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace CQRS.Security.Interfaces.Application
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(string email)
        {
            Identity = new GenericIdentity(email);
        }

        public IIdentity Identity { get; private set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Any(x => x.ToString("g") == role);
        }
    }
}
