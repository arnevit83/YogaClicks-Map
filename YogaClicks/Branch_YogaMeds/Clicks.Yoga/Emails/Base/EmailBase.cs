using System.Configuration;
using Postal;

namespace Clicks.Yoga.Emails
{
    public abstract class EmailBase : Email
    {
        public virtual string From
        {
            get { return ConfigurationManager.AppSettings["Clicks.Yoga.Emails.Sender"]; }
        }

        public abstract string To { get; }

        public string Subject { get; set; }
    }
}