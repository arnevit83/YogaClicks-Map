namespace Clicks.Yoga.Domain.Entities
{
    public class UserRole : Entity
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}