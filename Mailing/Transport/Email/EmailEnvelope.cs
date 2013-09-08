namespace Base.Mailing.Transport.Email
{
    using Content;
    using DDD.Domain.Annotations;
    using Transport;

    [DomainService]
    public class EmailEnvelope : IMessageEnvelope
    {
        public string To { get; private set; }

        public string ReplyTo { get; private set; }

        public string Subject { get; private set; }

        public IMessageContent Content { get; private set; }

        public EmailEnvelope(string subject, string to, string replyTo, IMessageContent content)
        {
            To = to;
            ReplyTo = replyTo;
            Subject = subject;
            Content = content;
        }
    }
}
