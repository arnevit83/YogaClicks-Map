namespace Clicks.Yoga.Domain.Entities
{
    public abstract class PrincipalEntity : Entity, ISecurable
    {
        protected PrincipalEntity()
        {
            Active = true;
        }

        public virtual User Owner { get; set; }

        int? ISecurable.OwnerId
        {
            get { return Owner == null ? null : (int?)Owner.Id; }
        }

        public bool Active { get; set; }
    }
}
