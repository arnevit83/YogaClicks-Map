using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class NewsStoryTypeModel : EntityModel<NewsStoryType>
    {
        public NewsStoryTypeModel(NewsStoryType entity) : base(entity) {}

        public string Tag { get; private set; }

        public bool Shareable { get; private set; }

        public override void Populate(NewsStoryType entity)
        {
            Tag = entity.Tag;
            Shareable = entity.Shareable;
        }
    }
}