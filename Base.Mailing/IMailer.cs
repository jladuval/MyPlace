namespace Base.Mailing
{
    public interface IMailer
    {
        void Run();

        void Send(int emailId);
    }
}
