using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.ServiceModel;
using System.Reflection;
using Common.ExceptionHandling.ServiceBehavior;
using System.ServiceModel.Channels;

namespace Common.ExceptionHandling.Custom.Wcf
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
                var str = new StringBuilder();
                var context = OperationContext.Current;
                var inner = ex.InnerException;
                var subject = HttpUtility.HtmlEncode(ex.Message);

                str.AppendLine("<html><body style=\"font-family: Calibri; font-size: 11pt; color: #000;\">");

                str = ReportMessage(str, ex);
                str = ReportDetails(str, ex);
                str = ReportStackTrace(str, ex);

                while (inner != null)
                {
                    str = ReportInnerException(str, inner);
                    inner = inner.InnerException;
                }

                if (context != null)
                {
                    str = ReportRequestMessage(str, context.RequestContext);
                    str = ReportRequestProperties(str, context.RequestContext);

                    subject = context.RequestContext.RequestMessage.Headers.Action;
                }

                if (subject.Length > 80)
                {
                    subject = subject.Substring(0, 40) + "..." + subject.Substring(subject.Length - 40);
                }

                str.AppendLine("</body></html>");

                SendMail("[" + Environment.MachineName + "] " + subject, str.ToString());
            }
            catch { }
        }

        private StringBuilder ReportMessage(StringBuilder str, Exception ex)
        {
            return str.AppendLine("<h2>" + HttpUtility.HtmlEncode(ex.Message) + "</h2>");
        }

        private StringBuilder ReportDetails(StringBuilder str, Exception ex)
        {
            str.AppendLine("<br />");
            str.AppendLine("<strong>Server Timestamp:</strong> " + DateTime.Now.ToString() + "<br />");
            str.AppendLine("<strong>Exception Type:</strong> " + ex.GetType().FullName + "<br />");
            str.AppendLine("<br />");
            str.AppendLine("<strong>Target Site:</strong> " + ex.TargetSite.Name + "<br />");

            return str;
        }

        private StringBuilder ReportStackTrace(StringBuilder str, Exception ex)
        {
            str.AppendLine("<br /><strong>Stack Trace:</strong><br />");
            str.AppendLine(ex.StackTrace.Replace(Environment.NewLine, "<br />"));

            return str;
        }

        private StringBuilder ReportInnerException(StringBuilder str, Exception ex)
        {
            str.AppendLine("<br />");
            str.AppendLine("<strong>Message:</strong> " + ex.Message + "<br />");
            str.AppendLine("<strong>Type:</strong>" + ex.GetType().FullName + "<br />");
            str.AppendLine("<strong>Stack Trace:</strong><br />");
            str.AppendLine(ex.StackTrace.Replace(Environment.NewLine, "<br />"));

            return str;
        }

        private StringBuilder ReportRequestProperties(StringBuilder str, RequestContext details)
        {
            str.AppendLine("<br /><br /><strong>Request Properties</strong><br />");

            foreach (var key in details.RequestMessage.Properties.Keys)
            {
                str.AppendLine("<strong>" + key + " {</strong><br />");
                foreach (var prop in ReflectionFunctions.GetPropertyValues(details.RequestMessage.Properties[key]))
                {
                    str.AppendLine("&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + prop.Key + "</strong>: " + HttpUtility.HtmlEncode(prop.Value).Replace(Environment.NewLine, "<br />") + "<br />");
                }
                str.AppendLine("<strong>}</strong><br />");
            }

            return str;
        }

        private StringBuilder ReportRequestMessage(StringBuilder str, RequestContext details)
        {
            str.AppendLine("<br /><strong>Request Message</strong><br />");

            str.AppendLine(HttpUtility.HtmlEncode(details.RequestMessage.ToString()).Replace(Environment.NewLine, "<br />"));

            return str;
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
