namespace Clicks.Yoga.Domain.Entities
{
    public class GalleryAssociation : Entity
    {
        public virtual Gallery Gallery { get; set; }

        public virtual EntityHandle Handle { get; set; }
    }
}