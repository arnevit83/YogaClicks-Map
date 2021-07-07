using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class UserEmailAddressChangeRequestEmail : EmailBase
    {
        public UserEmailAddressChangeRequest Request { get; set; }

        public override string To
        {
            get { return Request.EmailAddress; }
        }
    }
}