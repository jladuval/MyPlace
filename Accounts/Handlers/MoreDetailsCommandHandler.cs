﻿namespace Accounts.Handlers
{
    using Domain;
    using Interfaces.Commands;

    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;

    using GoogleMaps.LocationServices;

    using Infrastructure.NHibernate.Exceptions;
    using Infrastructure.NHibernate.Repositories;

    [CommandHandler]
    public class MoreDetailsCommandHandler : ICommandHandler<MoreDetailsCommand>
    {
        private readonly IRepository<User> _userRepository;

        public MoreDetailsCommandHandler(IRepository<User> userRepository)
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
                var latlng = GetLatLong(command.LocationToString());
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

        private MapPoint GetLatLong(string locationString)
        {
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(locationString);
            return point;
        }
    }
}
