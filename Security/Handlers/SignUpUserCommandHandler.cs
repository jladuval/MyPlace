namespace Security.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Infrastructure.NHibernate.Repositories;
    using Domain;
    using Interfaces.Commands;
    using Services;

    [CommandHandler]
    public class SignUpUserCommandHandler : ICommandHandler<SignUpUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        private readonly ICryptoService _cryptoService;

        private readonly UserFactory _userFactory;

        private readonly IRepository<Role> _roleRepository;

        public SignUpUserCommandHandler(
            IRepository<User> userRepository,
            ICryptoService cryptoService, 
            UserFactory userFactory,
            IRepository<Role> roleRepository)
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
                user.Roles = new List<Role> { _roleRepository.Find().Single(x => x.Name == "User") };
                user.VerificationCode = _cryptoService.GenerateRandomHash();
                _userRepository.Save(user);
                user.FinishedSignUp();
            }
        }
    }
}