namespace Security.Interfaces.Presentation
{
    using System;
    using System.Collections.Generic;

    public class CheckUserCredentialsDto
    {
        public Guid UserId { get; set; }

        public IList<string> Roles { get; set; }

        public bool HasDetails { get; set; }

        public string Email { get; set; }

        public bool IsVerified { get; set; }
    }
}
