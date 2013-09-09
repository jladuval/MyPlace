namespace Accounts.Handlers.Dinner
{
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Dinner;

    [CommandHandler]
    public class AddCommentToDinnerCommandHandler : ICommandHandler<AddCommentToDinnerCommand>
    {
        private readonly IRepository<Dinner> _dinnerRepository;
        private readonly IRepository<User> _userRepository;

        public AddCommentToDinnerCommandHandler(IRepository<Dinner> dinnerRepository, IRepository<User> userRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
        }

        public void Handle(AddCommentToDinnerCommand command)
        {
            var dinner = _dinnerRepository.Load(command.DinnerId);
            if (dinner == null) return;
            dinner.AddComment(new Comment
            {
                Text = command.Text,
                User = _userRepository.Load(command.UserId)
            });
        }
    }
}
