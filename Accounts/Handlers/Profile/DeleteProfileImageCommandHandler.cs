namespace Accounts.Handlers.Profile
{
    using System.Linq;

    using Accounts.Domain;
    using Accounts.Interfaces.Commands.Profile;

    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;

    using Infrastructure.NHibernate.Repositories;

    [CommandHandler]
    public class DeleteProfileImageCommandHandler : ICommandHandler<DeleteProfileImageCommand>
    {
        private readonly IRepository<User> _userRepository;

        public DeleteProfileImageCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(DeleteProfileImageCommand command)
        {
            var user = _userRepository.Load(command.UserId);

            if (user == null) return;

            var profileImage = user.ProfileImages.FirstOrDefault(x => x.FolderPath == command.FolderPath);

            if (profileImage == null) return;

            if (user.ProfileImageUrl == profileImage.Url) user.ProfileImageUrl = null;

            user.ProfileImages.Remove(profileImage);
        }
    }
}
