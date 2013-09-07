namespace Security.Handlers
{
    using System.Linq;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands;

    [CommandHandler]
    public class ActivateUserByIdCommandHandler : ICommandHandler<ActivateUserByIdCommand>
    {
        private readonly IRepository<User> _userRepository;

        public ActivateUserByIdCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(ActivateUserByIdCommand command)
        {
            _userRepository.Find().Single(x => x.Id == command.Id).VerificationCode = null;
        }
    }
}
