namespace Base.Mailing.Transport
{
    using Content;

    public interface IMessageEnvelopeFactory
    {
        IMessageEnvelope GetEnvelope(string subject, string to, IMessageContent message);
    }
}
