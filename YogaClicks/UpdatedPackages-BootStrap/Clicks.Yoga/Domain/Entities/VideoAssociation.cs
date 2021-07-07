namespace Clicks.Yoga.Domain.Entities
{
    public class VideoAssociation : Entity
    {
        public virtual Video Video { get; set; }

        public virtual EntityHandle Handle { get; set; }
    }
}