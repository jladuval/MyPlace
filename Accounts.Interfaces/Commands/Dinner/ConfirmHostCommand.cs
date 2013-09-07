namespace Accounts.Interfaces.Commands.Dinner
{
    public class ConfirmHostCommand
    {
        public string Token { get; set; }

        public ConfirmHostCommand(string token)
        {
            Token = token;
        }
    }
}
