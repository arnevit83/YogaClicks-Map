using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ReviewExperienceMapping : EntityMapping<ReviewExperience>
    {
        public ReviewExperienceMapping()
        {
            HasRequired(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId")).WillCascadeOnDelete(false);
            Property(e => e.FilterKey).HasMaxLength(100);
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}