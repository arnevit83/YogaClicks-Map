using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class EntityTypeMapping : EntityMapping<EntityType>
    {
        public EntityTypeMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(255);
            Property(e => e.SystemName).IsRequired().HasMaxLength(255);
            Property(e => e.DisplayName).IsRequired().HasMaxLength(100);
            Property(e => e.DisplayPlural).IsRequired().HasMaxLength(100);
            Property(e => e.Controller).HasMaxLength(100);
            Ignore(e => e.CanReview);
        }
    }
}