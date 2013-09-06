namespace Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using Mailing.Interfaces;

    public class TaskRunnerController : Controller
    {
        private readonly IMailer _mailer;

        public TaskRunnerController(IMailer mailer)
        {
            _mailer = mailer;
        }

        public ActionResult Mailer()
        {
            _mailer.Run();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
