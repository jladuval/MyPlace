namespace Base.Mailing
{
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using Content;
    using Content.Razor;
    using DDD.Domain.Annotations;
    using Entities;
    using global::Infrastructure.NHibernate.Repositories;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Transport;

    [DomainService]
    public class Mailer : IMailer 	
    {
        private readonly IMessageEnvelopeFactory _messageEnvelopeFactory;
        private readonly IMessageTransport _messageTransport;
        private readonly IRepository<Email> _emailRepository;

        public Mailer(IMessageEnvelopeFactory messageEnvelopeFactory, IMessageTransport messageTransport, IRepository<Email> emailRepository)
        {
            _messageEnvelopeFactory = messageEnvelopeFactory;
            _messageTransport = messageTransport;
            _emailRepository = emailRepository;
        }

        public void Run()
        {
            var emails = _emailRepository.Find().Where(x => x.SentDate == null).ToList();
            foreach (var email in emails)
            {
                Send(email);
                email.SentDate = DateTime.UtcNow;
            }
        }

        public void Send(int emailId)
        {
            Task.Factory.StartNew(() =>
            {
                var email = _emailRepository.Find().SingleOrDefault(x => x.Id == emailId);
                if (email != null)
                {
                    Send(email);
                    email.SentDate = DateTime.UtcNow;
                }
            });
        }

        private void Send(Email email)
        {
            var model = JsonConvert.DeserializeObject<ExpandoObject>(email.Payload, new ExpandoObjectConverter());
            IMessageContent message = null;
            message = new RazorMessage(email.TemplateName, model);
            
            var envelope = _messageEnvelopeFactory.GetEnvelope(GetSubject(email.TemplateName), email.Address, message);
            _messageTransport.Send(envelope);

        }

        private string GetSubject(string templateName)
        {
            switch (templateName)
            {
                case "Welcome":
                    return "Welcome";

                case "Reset Password":
                    return "Here is your new password";

                default:
                    return "You have a message";
            }
        }
    }
}
