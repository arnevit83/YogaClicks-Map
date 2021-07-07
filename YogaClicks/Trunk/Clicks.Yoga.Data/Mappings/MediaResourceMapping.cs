using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class MediaResourceMapping : EntityMapping<MediaResource>
    {
        public MediaResourceMapping()
        {
            HasRequired(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            Property(e => e.Uri).IsRequired().HasMaxLength(1000);
            Property(e => e.Identifier).HasMaxLength(1000);
            Property(e => e.EntityId);
            Property(e => e.Title);
            Property(e => e.Content);
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            Property(e => e.ExpiryTime);
        }
    }
}