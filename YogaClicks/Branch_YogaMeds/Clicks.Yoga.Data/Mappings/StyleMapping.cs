using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class StyleMapping : EntityMapping<Style>
    {
        public StyleMapping()
        {
            HasOptional(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId"));
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            Property(e => e.Founder).IsRequired().HasMaxLength(100);
            Property(e => e.FoundingTime).IsRequired().HasMaxLength(100);
            Property(e => e.Location).HasMaxLength(100);
            Property(e => e.Description).IsRequired();
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.DirectoryImage).WithMany().Map(a => a.MapKey("DirectoryImageId"));
            HasOptional(e => e.Website).WithMany().Map(a => a.MapKey("WebsiteId"));
            HasMany(e => e.Traits).WithMany(e => e.Styles).Map(a => a.MapLeftKey("StyleId").MapRightKey("TraitId").ToTable("StyleTraitLink"));
        }
    }
}