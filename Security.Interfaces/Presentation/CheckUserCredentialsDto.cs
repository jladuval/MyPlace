using System;
using System.Collections.Generic;
using Security.Interfaces.Application;

namespace Security.Interfaces.Presentation
{
    public class CheckUserCredentialsDto
    {
        public Guid UserId { get; set; }

        public IList<UserRoles> Roles { get; set; }
    }
}
