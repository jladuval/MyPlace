namespace Accounts.Handlers.Profile
{
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Exceptions;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Profile;
    using Services;

    [CommandHandler]
    public class MoreDetailsCommandHandler : ICommandHandler<MoreDetailsCommand>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILocationParsingService _locationParsingService;

        public MoreDetailsCommandHandler(IRepository<User> userRepository, ILocationParsingService locationParsingService)
        {
            _userRepository = userRepository;
            _locationParsingService = locationParsingService;
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
                user.Description = command.Description ?? user.Description;
                user.Age = command.Age ?? user.Age;
                user.Orientation = command.Orientation;
                user.Gender = command.Gender;
                user.Friendship = command.Friendship;
                user.Romance = command.Romance;
                var latlng = _locationParsingService.GetLatLong(command.Address, command.Suburb, command.City, command.Country, command.Postcode);
                user.Location = new Location(
                    command.Address,
                    command.Suburb,
                    command.City,
                    command.Country,
                    command.Postcode,
                    latlng.Latitude,
                    latlng.Longitude);
                _userRepository.Save(user);
            }
        }
    }
}
