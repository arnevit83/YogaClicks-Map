using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class VideoTypeMapping : EntityMapping<VideoType>
    {
        public VideoTypeMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.EmbedHtml).IsRequired();
        }
    }
}