using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Handlers.Dinner
{
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Dinner;

    [CommandHandler]
    public class ConfirmInvitationCommandHandler : ICommandHandler<ConfirmInvitationCommand>
    {
        private readonly IRepository<DinnerApplicant> _dinnerApplicantRepository;

        public ConfirmInvitationCommandHandler(IRepository<DinnerApplicant> dinnerApplicantRepository)
        {
            _dinnerApplicantRepository = dinnerApplicantRepository;
        }

        public void Handle(ConfirmInvitationCommand command)
        {
            Guid verificationToken;
            if (Guid.TryParse(command.Token, out verificationToken))
            {
                var dinnerApplicant =
                    _dinnerApplicantRepository.Find().FirstOrDefault(x => x.VerificationCode == verificationToken);
                if (dinnerApplicant != null)
                {
                    dinnerApplicant.VerificationCode = null;
                }
            }
        }
    }
}
