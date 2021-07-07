using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Registration
{
    public class RegisterCompleteModel
    {
        public RegisterCompleteModel(Teacher teacher, Venue venue)
        {
            if (teacher != null) Teacher = new TeacherModel(teacher);
            if (venue != null) Venue = new VenueModel(venue);
        }

        public TeacherModel Teacher { get; set; }

        public VenueModel Venue { get; set; }
    }
}