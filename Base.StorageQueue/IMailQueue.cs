namespace Base.AzureStorage
{
    public interface IMailQueue
    {
        void SendMessage<T>(T message) where T : class;
    }
}
