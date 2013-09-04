namespace Accounts.Handlers.Dinner
{
    using System;
    using System.Linq;

    using Domain;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Dinner;
    using NHibernate.Dialect.Function;

    using NHibernate.Linq;

    [CommandHandler]
    public class ApplyForDinnerCommandHandler : ICommandHandler<ApplyForDinnerCommand>
    {
        private readonly IRepository<Dinner> _dinnerRepository;
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<DinnerApplicant> _dinnerApplicantRepository;

        public ApplyForDinnerCommandHandler(IRepository<Dinner> dinnerRepository, IRepository<User> userRepository, IRepository<DinnerApplicant> dinnerApplicantRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
            _dinnerApplicantRepository = dinnerApplicantRepository;
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
                }
                dinner.Applicants.Add(application);
                user.AppliedDinners.Add(application);
            }
        }
    }
}
