namespace Accounts.Handlers.Applications
{
    using System.Linq;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Domain;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Applications;
    using NHibernate.Linq;

    [CommandHandler]
    public class AcceptApplicantCommandHandler : ICommandHandler<AcceptApplicantCommand>
    {
        private readonly IRepository<DinnerApplicant> _dinnerApplicantRepository;

        public AcceptApplicantCommandHandler(IRepository<DinnerApplicant> dinnerApplicantRepository)
        {
            _dinnerApplicantRepository = dinnerApplicantRepository;
        }

        public void Handle(AcceptApplicantCommand command)
        {
            var application = _dinnerApplicantRepository.Find()
                .Fetch(x => x.Dinner)
                .ThenFetch(x => x.User)
                .Fetch(x => x.User)
                .Fetch(x => x.Partner)
                .SingleOrDefault(x => x.Id == command.ApplicantId);

            if (application == null) return;

            if (application.Dinner.User.Id != command.UserId) return;

            application.Accepted = true;
        }
    }
}
