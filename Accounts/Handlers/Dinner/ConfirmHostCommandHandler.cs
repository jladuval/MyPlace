namespace Accounts.Handlers.Dinner
{
    using System;
    using System.Linq;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Dinner;

    [CommandHandler]
    public class ConfirmHostCommandHandler : ICommandHandler<ConfirmHostCommand>
    {
        private readonly IRepository<Dinner> _dinnerRepository;

        public ConfirmHostCommandHandler(IRepository<Dinner> dinnerRepository)
        {
            _dinnerRepository = dinnerRepository;
        }

        public void Handle(ConfirmHostCommand command)
        {
            Guid verificationToken;
            if (Guid.TryParse(command.Token, out verificationToken))
            {
                var dinner =
                    _dinnerRepository.Find().FirstOrDefault(x => x.VerificationCode == verificationToken);
                if (dinner != null)
                {
                    dinner.VerificationCode = null;
                }
            }
        }
    }
}
