using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherTrainingCourseVenueModel : EntityModel<TeacherTrainingCourseVenue>
    {
        public TeacherTrainingCourseVenueModel(TeacherTrainingCourseVenue entity) : base(entity) {}

        public VenueModel Venue { get; set; }

        public bool Confirmed { get; set; }

        public override void Populate(TeacherTrainingCourseVenue entity)
        {
            Venue = new VenueModel(entity.Venue);
            Confirmed = entity.Confirmed;
        }
    }
}