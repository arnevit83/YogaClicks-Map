namespace Clicks.Yoga.Domain.Entities
{
    public class UserEmailAddressChangeAction : Entity
    {
        public virtual UserEmailAddressChangeRequest Request { get; set; }

        public string ClientAddress { get; set; }
    }
}