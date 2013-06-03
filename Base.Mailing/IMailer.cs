namespace Base.Mailing
{
    public interface IMailer
    {
        void Send<T>(IMailMessage<T> message) where T : class;
    }
}
