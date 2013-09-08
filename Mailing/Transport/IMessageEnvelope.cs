namespace Base.Mailing.Transport
{
    using Content;

    public interface IMessageEnvelope
    {
        string To { get; }
        string ReplyTo { get; }
        string Subject { get; }
        IMessageContent Content { get; }
    }
}
