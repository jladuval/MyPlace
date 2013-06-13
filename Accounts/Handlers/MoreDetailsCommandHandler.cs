namespace Accounts.Handlers
{
    using Accounts.Domain;
    using Accounts.Interfaces.Commands;
    using Accounts.Repositories;

    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;

    using Infrastructure.NHibernate.Exceptions;
    using Infrastructure.NHibernate.Repositories;

    [CommandHandler]
    public class MoreDetailsCommandHandler : ICommandHandler<MoreDetailsCommand>
    {
        private readonly IUserRepository _userRepository;

        public MoreDetailsCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(MoreDetailsCommand command)
        {
            var user = _userRepository.Load(command.UserId);

            if (user == null)
                throw new EntityNotFoundException();

            using (new UnitOfWork())
            {
                user.FirstName = command.FirstName;
                user.LastName = command.LastName;
                user.Location = new Location(
                    command.Address,
                    command.Suburb,
                    command.City,
                    command.Country,
                    command.Postcode,
                    command.Latitude,
                    command.Longitude);
                _userRepository.Save(user);
            }
        }
    }
}
