namespace Base.Mailing.Transport.Email
{
    using System.Net.Mail;
    using DDD.Domain.Annotations;
    using Transport;

    [DomainService]
    public class EmailTransport : IMessageTransport
    {
        public void Send(IMessageEnvelope envelope)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(envelope.To));
            message.Subject = envelope.Subject;
            message.Body = envelope.Content.Content;
            message.IsBodyHtml = true;

            var client = new SmtpClient();
            client.Send(message);
        }
    }
}
