﻿namespace Accounts.Handlers.Profile
{
    using System.Linq;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Profile;

    [CommandHandler]
    public class SelectProfileImageCommandHandler : ICommandHandler<SelectProfileImageCommand>
    {
        private readonly IRepository<User> _userRepository;

        public SelectProfileImageCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(SelectProfileImageCommand command)
        {
            var user = _userRepository.Load(command.UserId);

            user.SetProfileImage(command.ImageName);
        }
    }
}
