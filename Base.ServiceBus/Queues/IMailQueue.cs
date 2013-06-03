namespace VLQR.Base.ServiceBus.Queues
{
    public interface IMailQueue
    {
        void SendMessage<T> (T message) where T : class;
    }
}
