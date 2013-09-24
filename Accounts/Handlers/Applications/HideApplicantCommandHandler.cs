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
    public class HideApplicantCommandHandler : ICommandHandler<HideApplicantCommand>
    {
        private readonly IRepository<DinnerApplicant> _dinnerApplicantRepository;

        public HideApplicantCommandHandler(IRepository<DinnerApplicant> dinnerApplicantRepository)
        {
            _dinnerApplicantRepository = dinnerApplicantRepository;
        }

        public void Handle(HideApplicantCommand command)
        {
            var application = _dinnerApplicantRepository.Find()
                .Fetch(x => x.Dinner)
                .ThenFetch(x => x.User)
                .Fetch(x => x.User)
                .Fetch(x => x.Partner)
                .SingleOrDefault(x => x.Id == command.ApplicationId);

            if (application == null) return;

            if (application.Dinner.User.Id != command.UserId) return;

            application.Hidden = true;
        }
    }
}
