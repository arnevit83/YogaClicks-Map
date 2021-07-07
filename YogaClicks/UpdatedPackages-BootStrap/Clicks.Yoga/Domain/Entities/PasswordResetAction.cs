namespace Clicks.Yoga.Domain.Entities
{
    public class PasswordResetAction : Entity
    {
        public virtual PasswordResetRequest Request { get; set; }

        public string ClientAddress { get; set; }
    }
}