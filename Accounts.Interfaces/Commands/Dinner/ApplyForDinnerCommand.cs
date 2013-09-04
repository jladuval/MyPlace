namespace Accounts.Interfaces.Commands.Dinner
{
    using System;

    public class ApplyForDinnerCommand
    {
        public readonly Guid UserId;

        public readonly Guid DinnerId;

        public string PartnerEmail { get; set; }

        public ApplyForDinnerCommand(Guid userId, Guid dinnerId)
        {
            UserId = userId;
            DinnerId = dinnerId;
        }
    }
}
