using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class VideoTypeModel : EntityModel<VideoType>
    {
        public VideoTypeModel(VideoType entity) : base(entity) {}

        public string Name { get; set; }

        public override void Populate(VideoType entity)
        {
            Name = entity.Name; 
        }
    }
}