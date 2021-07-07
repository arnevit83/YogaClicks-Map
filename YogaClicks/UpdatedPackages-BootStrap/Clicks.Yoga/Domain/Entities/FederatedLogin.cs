namespace Clicks.Yoga.Domain.Entities
{
    public class FederatedLogin : Entity
    {
        public virtual User User { get; set; }

        public virtual FederatedLoginProvider Provider { get; set; }

        public string Identifier { get; set; }
    }
}