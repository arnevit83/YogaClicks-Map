using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class InviteEntityEmail : EmailBase
    {
        public EntityHandleInvite Invite { get; set; }

        public override string To
        {
            get { return Invite.EmailAddress; }
        }
    }
}