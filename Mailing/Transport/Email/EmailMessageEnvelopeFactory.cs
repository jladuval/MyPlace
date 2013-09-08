namespace Base.Mailing.Transport.Email
{
    using Base.DDD.Domain.Annotations;
    using Base.Mailing.Content;
    using Base.Mailing.Transport;

    [DomainService]
    public class EmailMessageEnvelopeFactory : IMessageEnvelopeFactory
    {
        public IMessageEnvelope GetEnvelope(string subject, string to, IMessageContent message)
        {
            return new EmailEnvelope(subject, to, null, message);
        }
    }
}
