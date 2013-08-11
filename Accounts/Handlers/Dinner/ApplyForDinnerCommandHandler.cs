namespace Accounts.Handlers.Dinner
{
    using System.Linq;

    using Domain;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Infrastructure.NHibernate.Repositories;
    using Interfaces.Commands.Dinner;

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
            var dinner = _dinnerRepository.Find().Where(x => x.Id == command.DinnerId).Fetch(x => x.Applicants).Single();
            var user = _userRepository.Find().Where(x => x.Id == command.UserId).Fetch(x => x.AppliedDinners).Single();
            /*if (dinner != null && user != null)
                dinner.UserApplied(user);*/
            dinner.Applicants.Add(user);
            user.AppliedDinners.Add(dinner);
        }
    }
}
