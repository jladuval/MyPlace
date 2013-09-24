namespace Accounts.Interfaces.Commands.Applications
{
    using System;

    public class HideApplicantCommand
    {
        public Guid ApplicationId { get; private set; }

        public Guid UserId { get; private set; }

        public HideApplicantCommand(Guid applicationId, Guid userId)
        {
            ApplicationId = applicationId;
            UserId = userId;
        }
    }
}
