namespace Accounts.Handlers.Dinner
{
    using System;
    using System.Linq;
    using System.Web.Helpers;
    using Accounts.Domain;
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
        private readonly IRepository<Email> _emailRepository;

        public CreateDinnerCommandHandler(IRepository<Dinner> dinnerRepository, IRepository<User> userRepository, IRepository<Email> emailRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
            _emailRepository = emailRepository;
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
                    _emailRepository.Save(new Email
                    {
                        Address = partner.Email,
                        Priority = 1,
                        TemplateName = "ApproveHostDinner",
                        Payload = Json.Encode(new
                        {
                            VerificationUrl = command.HostUrl + "/Dinner/Approve?token=" + dinner.VerificationCode,
                            HostFirstName = dinner.User.FirstName,
                            HostLastName = dinner.User.LastName,
                            
                        })
                    });
                }

                _dinnerRepository.Save(dinner);
            }
        }
    }
}
