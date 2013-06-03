namespace Security.Commands.Handlers
{
    using System.Web;
    using System.Web.Security;

    using Base.CQRS.Commands.Handler;

    using Security.Interfaces.Commands;

    public class LogOffUserCommandHandler : ICommandHandler<LogOffUserCommand>
    {
        public void Handle(LogOffUserCommand command)
        {
            HttpContext.Current.Session["IsLoggedIn"] = false;
            FormsAuthentication.SignOut();
        }
    }
}
