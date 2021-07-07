using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class MediaResourceTypeMapping : EntityMapping<MediaResourceType>
    {
        public MediaResourceTypeMapping()
        {
            Property(e => e.Tag).IsRequired().HasMaxLength(255);
            HasOptional(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId"));
            Property(e => e.UriPattern).IsRequired().HasMaxLength(1000);
            Property(e => e.ScannerTypeName).IsRequired().HasMaxLength(1024);
            Property(e => e.ExpirySeconds);
        }
    }
}