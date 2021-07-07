using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GalleryMapping : PrincipalEntityMapping<Gallery>
    {
        public GalleryMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            HasOptional(e => e.Cover).WithMany().Map(a => a.MapKey("CoverImageId")).WillCascadeOnDelete(false);
        }
    }
}