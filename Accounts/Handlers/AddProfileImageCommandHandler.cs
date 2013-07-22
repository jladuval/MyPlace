using Accounts.Domain;
using Accounts.Interfaces.Commands;
using Base.CQRS.Commands.Handler;
using Infrastructure.NHibernate.Exceptions;
using Infrastructure.NHibernate.Repositories;

namespace Accounts.Handlers
{
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
