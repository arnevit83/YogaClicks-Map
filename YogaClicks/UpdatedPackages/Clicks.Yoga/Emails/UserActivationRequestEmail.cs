using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class UserActivationRequestEmail : EmailBase
    {
        public UserActivationRequest Request { get; set; }

        public override string To
        {
            get { return Request.User.EmailAddress; }
        }
    }
}