using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Clicks.Yoga.Domain.Services
{
    public class EmailService : ServiceBase, IEmailService
    {
        public EmailService(
             IEntityContext entityContext,
            ISecurityContext securityContext,
            IProfileService profileService
            )
            : base(entityContext, securityContext)
        {
            ProfileService = profileService;
        }

        private IProfileService ProfileService { get; set; }

        public void SendWelcomeEmail(User user)
        {
            var profile = ProfileService.GetProfile(user);
            var rm = new System.Resources.ResourceManager("Clicks.Yoga.Emails.Resources.Emails", this.GetType().Assembly);
            var body = rm.GetString("WelcomeEmail");
            body = body.Replace( "[NAME]",Capitalize(profile.Forename));
            SendEmail(user.EmailAddress, "Welcome to YogaClicks!", body);
        }

        private string Capitalize(string s)
        {
            if (s == null) return "";

            if (s.Length > 1) return char.ToUpper(s[0]) + s.Substring(1);

            return s.ToUpper();
        }

        public void SendEmail(string to, string subject, string body)
        {
            var email = new SmtpClient();
            var message = new MailMessage(ConfigurationManager.AppSettings["Clicks.Yoga.Emails.Sender"], to, subject, body);
            message.IsBodyHtml = true;
            email.Send(message);
        }
    }
}
