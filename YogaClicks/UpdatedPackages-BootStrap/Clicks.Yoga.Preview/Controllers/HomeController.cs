using System.Net;
using Clicks.Yoga.Preview.IO;
using Clicks.Yoga.Preview.Models;
using Clicks.Yoga.Preview.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Clicks.Yoga.Preview.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var classification = "Student";
            var file = FileContainer.Value.Students;

            if (model.IsTeacher)
            {
                classification = "Teacher";
                file = FileContainer.Value.Teachers;
            }
            else if (model.IsVenue)
            {
                classification = "Venue Manager";
                file = FileContainer.Value.Venues;
            }

            file.RecordEmailAddress(model.EmailAddress);

            using (var smtp = new SmtpClient())
            {
                using (var msg = new MailMessage(new MailAddress("support@purpletuesday.com"), new MailAddress(ConfigurationManager.AppSettings["notificationRecipient"])))
                {
                    msg.IsBodyHtml = false;

                    msg.Body = "New YogaClicks.com Registration Received!\r\n\r\nEmail Address: " + model.EmailAddress + "\r\nClassification: " + classification;

                    msg.Subject = "New YogaClicks.com Registration Received!";

                    smtp.Send(msg);
                }
            }

            using (var smtp = new SmtpClient("smtpout.europe.secureserver.net"))
            {
                smtp.Credentials = new NetworkCredential("info@yogaclicks.com", "12uksinfo");

                using (var msg = new MailMessage(new MailAddress("info@yogaclicks.com"), new MailAddress(model.EmailAddress)))
                {
                    msg.IsBodyHtml = true;

                    msg.Subject = "Welcome to YogaClicks";

                    DateTime date = new DateTime(2013, 10, 30, 12, 0, 0);

                    if (DateTime.Now < date)
                    {
                        msg.Body = Resources.PreDec;
                    }
                    else
                    {
                        msg.Body = Resources.DecOn;
                    }

                    smtp.Send(msg);
                }
            }

            return View("Thankyou");
        }
    }
}
