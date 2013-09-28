namespace Security.Handlers
{
    using System.Linq;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands;

    [CommandHandler]
    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public ActivateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(ActivateUserCommand command)
        {
            _userRepository.Find()
                .Where(x => x.VerificationCode == command.Token)
                .ToList()
                .ForEach(x => x.VerificationCode = null);
        }
    }
}
