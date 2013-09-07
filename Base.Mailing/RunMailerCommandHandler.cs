namespace Base.Mailing
{
    using CQRS.Commands.Attributes;
    using CQRS.Commands.Handler;
    using global::Mailing.Interfaces;

    [CommandHandler]
    public class RunMailerCommandHandler : ICommandHandler<RunMailerCommand>
    {
        private readonly IMailer _mailer;

        public RunMailerCommandHandler(IMailer mailer)
        {
            _mailer = mailer;
        }

        public void Handle(RunMailerCommand command)
        {
            _mailer.Run();
        }
    }
}
