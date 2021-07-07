using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class UserPasswordResetEmail : EmailBase
    {
        public PasswordResetRequest Request { get; set; }

        public override string To
        {
            get { return Request.Login.User.EmailAddress; }
        }
    }
}