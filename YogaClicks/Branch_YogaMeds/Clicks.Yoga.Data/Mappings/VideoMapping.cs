using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class VideoMapping : PrincipalEntityMapping<Video>
    {
        public VideoMapping()
        {
            HasRequired(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            Property(e => e.Description);
            Property(e => e.Length);
            HasOptional(e => e.AbilityLevel).WithMany().Map(a => a.MapKey("AbilityLevelId"));
            Property(e => e.IsClass).IsRequired();
        }
    }
}