using System.Configuration;
using System.Net.Mail;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Portal.Models.Facebook;

namespace Clicks.Yoga.Portal.Controllers
{
    public class FeedbackController : YogaController
    {
        public FeedbackController(
            IWorkUnit workUnit,
            ISecurityContext securityContext)
            : base(workUnit, securityContext) {}

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.FromEmail) || string.IsNullOrEmpty(model.FromName))
                {
                    return View();
                }

                using (var Msg = new MailMessage())
                {
                    Msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["Clicks.Yoga.Emails.Recipients.Admin"]));
                    Msg.From = new MailAddress(model.FromEmail, model.FromName);

                    Msg.Subject = "Feedback form submission";
                    Msg.IsBodyHtml = false;
                    Msg.Body = model.Message;

                    using (var MailObj = new SmtpClient())
                    {
                        MailObj.Send(Msg);
                    }

                }
            }

            return View("Success");

        }
    }
}