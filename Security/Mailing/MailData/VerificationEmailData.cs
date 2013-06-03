namespace Security.Mailing.MailData
{
    using System;

    [Serializable]
    public class VerificationEmailData 
    {
        public Guid UserId { get; set; }

        public string VerificationToken { get; set; }

        public VerificationEmailData(Guid userId, string verificationToken)
        {
            UserId = userId;
            VerificationToken = verificationToken;
        }
    }
}
