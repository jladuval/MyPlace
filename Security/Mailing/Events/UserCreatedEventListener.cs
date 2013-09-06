namespace Security.Mailing.Events
{
    using System.Web.Helpers;

    using Base.DDD.Infrastructure.Events;

    using Infrastructure.NHibernate.Repositories;

    using Security.Domain;
    using Security.Events;

    [EventListeners]
    public class UserCreatedEventListener : IEventListener<UserCreatedEvent>
    {
        private readonly IRepository<Email> _emailRepository;

        public UserCreatedEventListener(IRepository<Email> emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [EventListener(IsAsync = true)]
        public void Handle(UserCreatedEvent evt)
        {
            using (new UnitOfWork())
            {
                _emailRepository.Save(new Email
                {
                    Address = evt.Email,
                    Priority = 1,
                    TemplateName = "VerifyEmail",
                    Payload = Json.Encode(new
                    {
                        VerificationToken = evt.VerificationToken
                    })
                 });
            }
        }
    }
}
