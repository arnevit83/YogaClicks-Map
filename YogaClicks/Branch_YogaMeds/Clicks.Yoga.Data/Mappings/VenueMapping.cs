using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class VenueMapping : EntityMapping<Venue>
    {
        public VenueMapping()
        {
            HasOptional(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId"));
            Property(e => e.Name).IsRequired().HasMaxLength(70);
            Property(e => e.Description);
            Property(e => e.EmailAddress);
            HasOptional(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            HasOptional(e => e.Address).WithMany().Map(a => a.MapKey("AddressId"));
            HasMany(e => e.Websites).WithMany().Map(a => a.MapLeftKey("VenueId").MapRightKey("WebsiteId").ToTable("VenueWebsite"));
            HasOptional(e => e.Blog).WithMany().Map(a => a.MapKey("BlogId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
            HasMany(e => e.Styles).WithMany().Map(a => a.MapLeftKey("VenueId").MapRightKey("StyleId").ToTable("VenueStyleLink"));
            HasMany(e => e.Services).WithMany().Map(a => a.MapLeftKey("VenueId").MapRightKey("ServiceId").ToTable("VenueServiceLink"));
            HasMany(e  => e.Amenities).WithMany().Map(a => a.MapLeftKey("VenueId").MapRightKey("AmenityId").ToTable("VenueAmenityLink"));
            Property(e => e.Stubbed).IsRequired();
        }
    }
}