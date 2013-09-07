namespace Accounts.Interfaces.Commands.Dinner
{
    public class ConfirmInvitationCommand
    {
        public string Token { get; set; }

        public ConfirmInvitationCommand(string token)
        {
            Token = token;
        }
    }
}
