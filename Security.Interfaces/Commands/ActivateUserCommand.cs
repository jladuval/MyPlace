namespace Security.Interfaces.Commands
{
    public class ActivateUserCommand
    {
        public string Token { get; private set; }

        public ActivateUserCommand(string token)
        {
            Token = token;
        }
    }
}
