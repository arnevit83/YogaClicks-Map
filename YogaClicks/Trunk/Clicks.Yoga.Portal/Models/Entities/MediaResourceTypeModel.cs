using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class MediaResourceTypeModel : EntityModel<MediaResourceType>
    {
        public MediaResourceTypeModel(MediaResourceType entity) : base(entity) { }

        public string Tag { get; set; }

        public override void Populate(MediaResourceType entity)
        {
            Tag = entity.Tag;
        }
    }
}