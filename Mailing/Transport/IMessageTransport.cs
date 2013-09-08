namespace Base.Mailing.Transport
{
    public interface IMessageTransport
    {
        void Send(IMessageEnvelope envelope);
    }
}
