using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class PoseMapping : EntityMapping<Pose>
    {
        public PoseMapping()
        {
            Property(e => e.EnglishName).IsRequired().HasMaxLength(100);
            Property(e => e.SanskritName).IsRequired().HasMaxLength(100);
            Property(e => e.Roots).IsRequired();
            Property(e => e.Pronounciation).IsRequired().HasMaxLength(100);
            Property(e => e.Instructions).IsRequired();
            Property(e => e.Effects).IsRequired();
            Property(e => e.Tips).IsRequired();
            Property(e => e.Indications).IsRequired();
            Property(e => e.Contraindications).IsRequired();
            Property(e => e.MainVideo).HasMaxLength(100);
            HasRequired(e => e.AbilityLevel).WithMany().Map(a => a.MapKey("AbilityLevelId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.Video).WithMany().Map(a => a.MapKey("VideoId"));
            HasRequired(e => e.Category).WithMany(e => e.Poses).Map(a => a.MapKey("CategoryId"));
       
        }
    }
}