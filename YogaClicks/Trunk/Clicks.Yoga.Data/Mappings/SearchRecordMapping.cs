using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class SearchRecordMapping : EntityMapping<SearchRecord>
    {
        public SearchRecordMapping()
        {
            HasRequired(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId"));
            Property(e => e.EntityId).IsRequired();
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Description).IsRequired().IsMaxLength();
            Property(e => e.City).HasMaxLength(100);
            Property(e => e.Area).HasMaxLength(100);
            Property(e => e.Country).HasMaxLength(100);
            Property(e => e.Postcode).HasMaxLength(20);
            Property(e => e.StylesText).HasMaxLength(1000).HasColumnName("Styles");
            HasMany(e => e.Styles).WithMany().Map(a => a.MapLeftKey("RecordId").MapRightKey("StyleId").ToTable("SearchRecordStyleLink"));
            HasMany(e => e.Teachers).WithMany().Map(a => a.MapLeftKey("RecordId").MapRightKey("TeacherId").ToTable("SearchRecordTeacherLink"));
            HasMany(e => e.TeacherServices).WithMany().Map(a => a.MapLeftKey("RecordId").MapRightKey("TeacherServiceId").ToTable("SearchRecordTeacherServiceLink"));
            HasMany(e => e.Venues).WithMany().Map(a => a.MapLeftKey("RecordId").MapRightKey("VenueId").ToTable("SearchRecordVenueLink"));
            HasOptional(e => e.VenueType).WithMany().Map(a => a.MapKey("VenueTypeId"));
            HasMany(e => e.VenueServices).WithMany().Map(a => a.MapLeftKey("RecordId").MapRightKey("VenueServiceId").ToTable("SearchRecordVenueServiceLink"));
            HasMany(e => e.AccreditationBodies).WithMany().Map(a => a.MapLeftKey("RecordId").MapRightKey("AccreditationBodyId").ToTable("SearchRecordAccreditationBodyLink"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            Ignore(e => e.Location);
            Property(e => e.LocationGeography).HasColumnName("Location");
            Property(e => e.Date);
            Property(e => e.Rating).HasPrecision(2, 1);
            Property(e => e.Stubbed).IsRequired();
            HasOptional(e => e.Parent).WithMany().Map(a => a.MapKey("ParentId"));
            Ignore(e => e.LinkedEntities);
            Ignore(e => e.ImageExtension);
            Ignore(e => e.ImageGuid);
            Ignore(e => e.ImagePath);
            Ignore(e => e.OwnerId);
        }
    }
}