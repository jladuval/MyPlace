namespace Accounts.Interfaces.Commands.Applications
{
    using System;

    public class AcceptApplicantCommand
    {
        public Guid ApplicantId { get; private set; }

        public Guid UserId { get; private set; }

        public AcceptApplicantCommand(Guid applicantId, Guid userId)
        {
            ApplicantId = applicantId;
            UserId = userId;
        }
    }
}
