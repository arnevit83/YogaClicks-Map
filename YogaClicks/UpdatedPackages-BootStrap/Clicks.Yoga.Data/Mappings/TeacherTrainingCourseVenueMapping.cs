using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    class TeacherTrainingCourseVenueMapping : EntityMapping<TeacherTrainingCourseVenue>
    {
        public TeacherTrainingCourseVenueMapping()
        {
            HasRequired(e => e.Course).WithMany(e => e.Venues).Map(a => a.MapKey("CourseId"));
            HasRequired(e => e.Venue).WithMany().Map(a => a.MapKey("VenueId"));
            Property(e => e.Confirmed).IsRequired();
        }
    }
}