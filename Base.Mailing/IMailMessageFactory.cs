namespace Base.Mailing
{
    public interface IMailMessageFactory
    {
        IMailMessage<T> Create<T>(T model) where T : class;

        IMailMessage<T> Create<T>() where T : class;
    }
}
