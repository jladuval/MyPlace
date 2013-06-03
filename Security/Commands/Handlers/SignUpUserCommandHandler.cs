namespace Security.Commands.Handlers
{
    using System.Collections.Generic;

    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;

    using Infrastructure.NHibernate.Repositories;

    using Security.Domain;
    using Security.Interfaces.Commands;
    using Security.Services;

    [CommandHandler]
    public class SignUpUserCommandHandler : ICommandHandler<SignUpUserCommand>
    {
        private readonly IUserRepository _userRepository;

        private readonly ICryptoService _cryptoService;

        private readonly UserFactory _userFactory;

        private readonly IRoleRepository _roleRepository;

        public SignUpUserCommandHandler(
            IUserRepository userRepository,
            ICryptoService cryptoService, 
            UserFactory userFactory,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _userFactory = userFactory;
            _roleRepository = roleRepository;
        }

        public void Handle(SignUpUserCommand command)
        {
            using (new UnitOfWork())
            {
                var salt = _cryptoService.GenerateSalt();
                var user = _userFactory.CreateUser(command.Email, _cryptoService.Hash(command.Password, salt), salt);
                user.Roles = new List<Role> { _roleRepository.LoadByName("User") };
                user.VerificationCode = _cryptoService.GenerateRandomHash();
                _userRepository.Save(user);
                user.FinishedSignUp();
            }
        }
    }
}