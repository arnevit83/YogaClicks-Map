using System.Configuration;

namespace Clicks.Yoga.Emails
{
    public class AdminFeedbackEmail : EmailBase
    {
        public AdminFeedbackEmail(string senderName, string senderEmailAddress, string message)
        {
            SenderName = senderName;
            SenderEmailAddress = senderEmailAddress;
            Message = message;
        }

        public string SenderName { get; private set; }

        public string SenderEmailAddress { get; private set; }

        public string Message { get; private set; }

        public override string To
        {
            get { return ConfigurationManager.AppSettings["Clicks.Yoga.Emails.Recipients.Admin"]; }
        }
    }
}