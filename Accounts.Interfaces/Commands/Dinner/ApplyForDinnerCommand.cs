namespace Accounts.Interfaces.Commands.Dinner
{
    using System;

    public class ApplyForDinnerCommand
    {
        public readonly Guid UserId;

        public readonly Guid DinnerId;

        public ApplyForDinnerCommand(Guid userId, Guid dinnerId)
        {
            UserId = userId;
            DinnerId = dinnerId;
        }
    }
}
