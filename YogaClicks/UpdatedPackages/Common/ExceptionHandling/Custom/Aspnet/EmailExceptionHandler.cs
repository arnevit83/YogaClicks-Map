using System;
using System.Net.Mail;
using System.Web;

namespace Common.ExceptionHandling.Custom.Aspnet
{
    public class EmailExceptionHandler : IExceptionHandler
    {
        public void Handle(Exception ex)
        {
            if (!ConfigurationFunctions.GetAppSetting<bool>("EmailExceptionHandler.SendEmails"))
            {
                return;
            }

            try
            {
                var context = HttpContext.Current;
                var subject = HttpUtility.HtmlEncode(ex.Message);
                var body = new HtmlExceptionRenderer().Render(ex);

                if (context != null)
                {
                    subject = context.Request.Url.AbsolutePath;
                }

                if (subject.Length > 50)
                {
                    subject = subject.Substring(0, 25) + "..." + subject.Substring(subject.Length - 25);
                }

                SendMail("[" + Environment.MachineName + "] " + subject, body);
            }
            catch { }
        }

        private void SendMail(string subject, string message)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(ConfigurationFunctions.GetAppSetting("EmailExceptionHandler.Sender"));
                mail.To.Add(new MailAddress(ConfigurationFunctions.GetAppSetting("EmailExceptionHandler.To")));

                mail.Subject = subject;
                mail.Body = message;

                mail.IsBodyHtml = true;

                using (var client = new SmtpClient())
                {
                    client.Send(mail);
                }
            }
        }
    }
}
