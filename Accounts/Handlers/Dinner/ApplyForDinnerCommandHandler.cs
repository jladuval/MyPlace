namespace Accounts.Handlers.Dinner
{
    using Domain;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Dinner;

    [CommandHandler]
    public class ApplyForDinnerCommandHandler : ICommandHandler<ApplyForDinnerCommand>
    {
        private readonly IRepository<Dinner> _dinnerRepository;
        private readonly IRepository<User> _userRepository;

        public ApplyForDinnerCommandHandler(IRepository<Dinner> dinnerRepository, IRepository<User> userRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
        }

        public void Handle(ApplyForDinnerCommand command)
        {
            var dinner = _dinnerRepository.Load(command.DinnerId);
            var user = _userRepository.Load(command.UserId);
            if (dinner != null && user != null)
                dinner.UserApplied(user);
        }
    }
}
