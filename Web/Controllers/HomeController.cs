namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Base.CQRS.Commands;

    using Security.Interfaces.Commands;
    using Security.Interfaces.Queries;

    public class HomeController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;

        public HomeController(ISecurityUserReader securityUserReader, IGate gate)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
        }        

        public ActionResult Index()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _gate.Dispatch(new SignUpUserCommand(username,password));
            _securityUserReader.CheckUserCredentials(
                new CheckUserCredentialsQuery { Email = username, Password = password });
            return View();
        }
    }
}
