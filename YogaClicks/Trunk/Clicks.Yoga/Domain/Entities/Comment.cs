namespace Clicks.Yoga.Domain.Entities
{
    public class Comment : Entity, ISecurable
    {
        public virtual User Owner { get; set; }

        public virtual EntityHandle ActorHandle { get; set; }

        public string Content { get; set; }

        public int? OwnerId
        {
            get { return Owner == null ? (int?)null : Owner.Id; }
        }
    }
}