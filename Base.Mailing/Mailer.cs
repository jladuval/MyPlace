using Base.AzureStorage;
using Base.DDD.Domain.Annotations;

namespace Base.Mailing
{
    [DomainService]
    public class Mailer : IMailer
    {
        public IMailQueue MailQueue { get; set; }

        public void Send<T>(IMailMessage<T> message) where T : class
        {
            MailQueue.SendMessage(message);
        }
    }
}
