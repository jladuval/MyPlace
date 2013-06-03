using CQRS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Base.Entities.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Salt { get; set; }

        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; }

        public User()
        {
        }

        public User(string email, string password, string salt)
        {
            Email = email;
            Password = password;
            Salt = salt;
        }
    }
}
