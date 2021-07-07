namespace Clicks.Yoga.Domain.Entities
{
    public class FederatedLoginProvider : Entity
    {
        public string Tag { get; set; }

        public string Name { get; set; }

        public bool IsFacebook { get; set; }
    }
}