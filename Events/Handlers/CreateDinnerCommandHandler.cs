namespace Events.Handlers
{
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;

    using Domain;
    using Interfaces.Commands;
    using Repositories;

    using Infrastructure.NHibernate.Exceptions;
    using Infrastructure.NHibernate.Repositories;

    [CommandHandler]
    public class CreateDinnerCommandHandler : ICommandHandler<CreateDinnerCommand>
    {
        private readonly IDinnerRepository _dinnerRepository;

        private readonly IUserRepository _userRepository;

        public CreateDinnerCommandHandler(IDinnerRepository dinnerRepository, IUserRepository userRepository)
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

                _dinnerRepository.Save(
                    new Dinner(
                        user,
                        user.Location,
                        command.Starter,
                        command.Main,
                        command.Dessert,
                        command.Dry,
                        command.Description,
                        command.Date));
            }
        }
    }
}
