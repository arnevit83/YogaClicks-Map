using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class WebsiteMapping : EntityMapping<Website>
    {
        public WebsiteMapping()
        {
            Property(e => e.Url).IsRequired().HasMaxLength(255);
        }
    }
}