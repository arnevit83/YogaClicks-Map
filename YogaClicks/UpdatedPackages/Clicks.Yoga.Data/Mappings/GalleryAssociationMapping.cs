using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GalleryAssociationMapping : EntityMapping<GalleryAssociation>
    {
        public GalleryAssociationMapping()
        {
            HasRequired(e => e.Gallery).WithMany(e => e.Associations).Map(a => a.MapKey("GalleryId")).WillCascadeOnDelete(false);
            HasRequired(e => e.Handle).WithMany().Map(a => a.MapKey("HandleId")).WillCascadeOnDelete(false);
        }
    }
}