using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class InviteFriendEmail : EmailBase
    {
        public InviteFriendEmail(Invitation invitation)
        {
            Invitation = invitation;
        }

        public Invitation Invitation { get; set; }

        public override string To
        {
            get { return Invitation.RecipientIdentifier; }
        }
    }
}