using Base.CQRS.Commands.Attributes;

namespace Security.Interfaces.Commands
{
    [Command]
    public class SignUpUserCommand
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public SignUpUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
