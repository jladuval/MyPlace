namespace Events.Handlers
{
    using System;
    using System.Linq;

    using Accounts.Domain;
    using Accounts.Interfaces.Commands;
    using Accounts.Interfaces.Commands.Dinner;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Infrastructure.NHibernate.Exceptions;
    using Infrastructure.NHibernate.Repositories;

    [CommandHandler]
    public class CreateDinnerCommandHandler : ICommandHandler<CreateDinnerCommand>
    {
        private readonly IRepository<Dinner> _dinnerRepository;

        private readonly IRepository<User> _userRepository;

        public CreateDinnerCommandHandler(IRepository<Dinner> dinnerRepository, IRepository<User> userRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
        }

        public void Handle(CreateDinnerCommand command)
        {
            using (new UnitOfWork())
            {
                var user = _userRepository.Load(command.UserId);
                if (user == null) throw new EntityNotFoundException();

                var dinner = new Dinner(
                    user,
                    user.Location,
                    command.Starter,
                    command.Main,
                    command.Dessert,
                    command.Dry,
                    command.Description,
                    command.Date);

                if (command.PartnerEmail != null)
                {
                    var partner = _userRepository.Find().Single(x => x.Email == command.PartnerEmail);
                    dinner.VerificationCode = Guid.NewGuid();
                    dinner.Partner = partner;
                }

                _dinnerRepository.Save(dinner);
            }
        }
    }
}
