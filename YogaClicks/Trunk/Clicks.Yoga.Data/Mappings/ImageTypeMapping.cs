using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ImageTypeMapping : EntityMapping<ImageType>
    {
        public ImageTypeMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            Property(e => e.MimeType).IsRequired().HasMaxLength(50).IsUnicode(false);
            Property(e => e.Extension).IsRequired().HasMaxLength(3).IsUnicode(false);
        }
    }
}