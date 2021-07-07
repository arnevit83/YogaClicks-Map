using System;
using Clicks.Yoga.Emails;

namespace Clicks.Yoga.Domain.Entities
{
    public class PasswordResetRequest : Entity
    {
        public static readonly TimeSpan ValidityPeriod = new TimeSpan(1, 0, 0);

        public PasswordResetRequest()
        {
            Key = Guid.NewGuid().ToString();
            ExpiryTime = DateTime.Now.Add(ValidityPeriod);
        }

        public virtual PasswordLogin Login { get; set; }

        public string Key { get; set; }

        public string ClientAddress { get; set; }

        public bool Completed { get; set; }

        public DateTime ExpiryTime { get; set; }

        public bool Expired
        {
            get { return DateTime.Now >= ExpiryTime; }
        }

        public void SendEmail()
        {
            UserPasswordResetEmail email = new UserPasswordResetEmail { Request = this };
            email.Send();
        }
    }
}