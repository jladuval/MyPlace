using Base.DDD.Domain.Annotations;

namespace Base.Mailing
{
    [DomainFactory]
    public class MailMessageFactory : IMailMessageFactory
    {
        public IMailMessage<T> Create<T>(T model) where T : class
        {
            return new MailMessage<T>(model);
        }

        public IMailMessage<T> Create<T>() where T : class
        {
            return new MailMessage<T>();
        }
    }
}
