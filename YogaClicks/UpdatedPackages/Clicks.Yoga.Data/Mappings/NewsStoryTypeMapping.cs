using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NewsStoryTypeMapping : EntityMapping<NewsStoryType>
    {
        public NewsStoryTypeMapping()
        {
            Property(e => e.Tag).IsRequired().HasMaxLength(255);
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Vain).IsRequired();
            Property(e => e.Deduped).IsRequired();
        }
    }
}