using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class EntityHandleMapping : EntityMapping<EntityHandle>
    {
        public EntityHandleMapping()
        {
            Property(e => e.EntityId).IsRequired();
            HasRequired(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId"));
            Property(e => e.Name).HasMaxLength(4000);
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            Property(e => e.Location).HasMaxLength(100);
            HasOptional(e => e.Parent).WithMany().Map(a => a.MapKey("ParentId"));
            HasOptional(e => e.Owner).WithMany(e => e.Owned).Map(a => a.MapKey("OwnerId"));
            Property(e => e.Active).IsRequired();
        }
    }
}