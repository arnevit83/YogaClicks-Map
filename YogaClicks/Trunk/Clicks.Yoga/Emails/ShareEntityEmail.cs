using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class ShareEntityEmail : EmailBase
    {
        public ShareEntityEmail(IActor actor, EntityHandle handle, string emailAddress, string message)
        {
            Sender = actor;
            Handle = handle;
            EmailAddress = emailAddress;
            Message = message;
        }

        public IActor Sender { get; set; }

        public EntityHandle Handle { get; set; }

        public string EmailAddress { get; set; }

        public string Message { get; set; }

        public override string To
        {
            get { return EmailAddress; }
        }
    }
}