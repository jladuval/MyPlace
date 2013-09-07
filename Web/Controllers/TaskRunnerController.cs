namespace Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Base.CQRS.Commands;
    using Mailing.Interfaces;

    public class TaskRunnerController : Controller
    {
        private readonly IGate _gate;

        public TaskRunnerController(IGate gate)
        {
            _gate = gate;
        }

        public ActionResult Mailer()
        {
            _gate.Dispatch(new RunMailerCommand());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
