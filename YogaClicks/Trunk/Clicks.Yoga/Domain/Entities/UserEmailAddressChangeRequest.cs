using System;
using System.Linq;
using Clicks.Yoga.Context;

namespace Clicks.Yoga.Domain.Entities
{
    public class UserEmailAddressChangeRequest : Entity
    {
        public static readonly TimeSpan ValidityPeriod = new TimeSpan(1, 0, 0);

        public UserEmailAddressChangeRequest() {}

        public UserEmailAddressChangeRequest(User user, string address, ISecurityContext context)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (context == null)
                throw new ArgumentNullException("context");

            User = user;
            ClientAddress = context.ClientAddress;
            Key = Guid.NewGuid().ToString();
            EmailAddress = address;
            ExpiryTime = DateTime.Now.Add(ValidityPeriod);
        }
        
        public virtual User User { get; set; }

        public string ClientAddress { get; set; }

        public string Key { get; set; }

        public string EmailAddress { get; set; }

        public DateTime ExpiryTime { get; set; }

        public virtual UserEmailAddressChangeAction Action { get; set; }

        public bool Expired
        {
            get { return DateTime.Now >= ExpiryTime; }
        }

        public bool Completed
        {
            get { return Action != null; }
        }

        public void Complete(ISecurityContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var old = User.EmailAddress;

            User.EmailAddress = EmailAddress;

            Action = new UserEmailAddressChangeAction {
                Request = this,
                ClientAddress = context.ClientAddress
            };

            foreach (var login in User.PasswordLogins.Where(l => l.Username == old))
            {
                login.Username = User.EmailAddress;
            }
        }
    }
}