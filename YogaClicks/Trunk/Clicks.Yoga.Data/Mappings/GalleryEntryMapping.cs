using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GalleryEntryMapping : EntityMapping<GalleryEntry>
    {
        public GalleryEntryMapping()
        {
            HasRequired(e => e.Gallery).WithMany(e => e.Entries).Map(a => a.MapKey("GalleryId"));
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            HasRequired(e => e.Image).WithMany().Map(a => a.MapKey("ImageId")).WillCascadeOnDelete(false);
            Property(e => e.Promoted).IsRequired();
        }
    }
}