using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class GalleryEntryModel : EntityModel<GalleryEntry>, ISecurable
    {
        public GalleryEntryModel(GalleryEntry entity) : base(entity) {}

        public string Name { get; private set; }

        public ImageModel Image { get; private set; }

        public bool Promoted { get; private set; }

        public int? OwnerId { get; private set; }

        public override void Populate(GalleryEntry entity)
        {
            Name = entity.Name;
            Image = new ImageModel(entity.Image);
            Promoted = entity.Promoted;
            OwnerId = entity.Gallery.Owner.Id;
        }
    }
}