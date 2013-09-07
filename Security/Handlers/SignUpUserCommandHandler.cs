namespace Security.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Helpers;
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
        private readonly IRepository<Email> _emailRepository;

        public SignUpUserCommandHandler(
            IRepository<User> userRepository,
            ICryptoService cryptoService, 
            UserFactory userFactory,
            IRepository<Role> roleRepository,
            IRepository<Email> emailRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _userFactory = userFactory;
            _roleRepository = roleRepository;
            _emailRepository = emailRepository;
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
                _emailRepository.Save(new Email
                {
                    Address = user.Email,
                    Priority = 1,
                    TemplateName = "VerifyEmail",
                    Payload = Json.Encode(new
                    {
                        VerificationUrl = command.HostPath + "?token=" + user.VerificationCode
                    })
                });
                user.FinishedSignUp(command.HostPath);
            }
        }
    }
}