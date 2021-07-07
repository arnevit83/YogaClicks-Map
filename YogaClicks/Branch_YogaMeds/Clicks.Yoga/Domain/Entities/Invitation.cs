namespace Clicks.Yoga.Domain.Entities
{
    public class Invitation : Entity
    {
        public virtual Profile Sender { get; set; }

        public virtual FederatedLoginProvider LoginProvider { get; set; }

        public string RequestIdentifier { get; set; }

        public string RecipientIdentifier { get; set; }
    }
}