namespace Accounts.Interfaces.Commands.Dinner
{
    using System;

    public class AddCommentToDinnerCommand
    {
        public Guid DinnerId { get; private set; }
        public string Text { get; private set; }
        public Guid UserId { get; private set; }

        public AddCommentToDinnerCommand(Guid dinnerId, string text, Guid userId)
        {
            DinnerId = dinnerId;
            Text = text;
            UserId = userId;
        }
    }
}
