namespace Accounts.Handlers.Applications
{
    using System;
    using System.Linq;
    using System.Web.Helpers;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Applications;

    [CommandHandler]
    public class ApplyForDinnerCommandHandler : ICommandHandler<ApplyForDinnerCommand>
    {
        private readonly IRepository<Dinner> _dinnerRepository;
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<DinnerApplicant> _dinnerApplicantRepository;
        private readonly IRepository<Email> _emailRepository;

        public ApplyForDinnerCommandHandler(
            IRepository<Dinner> dinnerRepository, 
            IRepository<User> userRepository, 
            IRepository<DinnerApplicant> dinnerApplicantRepository, 
            IRepository<Email> emailRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
            _dinnerApplicantRepository = dinnerApplicantRepository;
            _emailRepository = emailRepository;
        }

        public void Handle(ApplyForDinnerCommand command)
        {
            using (new UnitOfWork())
            {
                var dinner = _dinnerRepository.Load(command.DinnerId);
                var user = _userRepository.Load(command.UserId);
                if (dinner == null || user == null) return;
                if (_dinnerApplicantRepository.Find().SingleOrDefault(x => (x.User == user || x.Partner == user) && x.Dinner == dinner) != null) return;
                var application = new DinnerApplicant(user, dinner);
                if (!string.IsNullOrEmpty(command.PartnerEmail))
                {
                    application.VerificationCode = Guid.NewGuid();
                    application.Partner = _userRepository.Find().Single(x => x.Email == command.PartnerEmail);
                    var email = new Email
                    {
                        Address = application.Partner.Email,
                        Priority = 1,
                        TemplateName = "ConfirmInvitation",
                        Payload = Json.Encode(new
                        {
                            VerificationUrl = command.ConfirmUrl + "?token=" + application.VerificationCode
                        })
                    };
                    _emailRepository.Save(email);
                }
                dinner.Applicants.Add(application);
                user.AppliedDinners.Add(application);
            }
        }
    }
}
