namespace Base.Mailing.Transport.Email
{
    using DDD.Domain.Annotations;
    using Content;
    using Transport;

    [DomainService]
    public class EmailMessageEnvelopeFactory : IMessageEnvelopeFactory
    {
        public IMessageEnvelope GetEnvelope(string subject, string to, IMessageContent message)
        {
            return new EmailEnvelope(subject, to, null, message);
        }
    }
}
