namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherTrainingCourseVenue : Entity
    {
        public TeacherTrainingCourseVenue() {}

        public TeacherTrainingCourseVenue(Venue venue)
        {
            Venue = venue;
        }

        public virtual TeacherTrainingCourse Course { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual bool Confirmed { get; set; }
    }
}