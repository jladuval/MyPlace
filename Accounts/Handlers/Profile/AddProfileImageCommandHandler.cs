namespace Accounts.Handlers
{
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Exceptions;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands;
    using Interfaces.Commands.Profile;

    [CommandHandler]
    public class AddProfileImageCommandHandler : ICommandHandler<AddProfileImageCommand>
    {
        private readonly IRepository<User> _userRepository;

        public AddProfileImageCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(AddProfileImageCommand command)
        {
            var user = _userRepository.Load(command.UserId);

            if (user == null)
                throw new EntityNotFoundException();

            user.AddProfileImage(new Image(command.ImageUrl, command.FolderPath, command.ImageName));
        }
    }
}
