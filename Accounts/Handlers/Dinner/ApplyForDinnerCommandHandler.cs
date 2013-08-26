namespace Accounts.Handlers.Dinner
{
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

        public ApplyForDinnerCommandHandler(IRepository<Dinner> dinnerRepository, IRepository<User> userRepository)
        {
            _dinnerRepository = dinnerRepository;
            _userRepository = userRepository;
        }

        public void Handle(ApplyForDinnerCommand command)
        {
            using (new UnitOfWork())
            {
                var dinner = _dinnerRepository.Load(command.DinnerId);
                var user = _userRepository.Load(command.UserId);
                if (dinner != null && user != null)
                {
                    var application = new DinnerApplicant(user, dinner);
                    dinner.Applicants.Add(application);
                    user.AppliedDinners.Add(application);
                }
            }
        }
    }
}
