using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class TeacherTrainingCourseVenueChooserModel : CreateCourseVenueChooserModel
    {
        public TeacherTrainingCourseVenueChooserModel() {}

        public TeacherTrainingCourseVenueChooserModel(ICollection<Venue> entities) : base(entities) { }

        public void PopulateEntity(TeacherTrainingCourse course, ITeacherTrainingService teacherTrainingService, IVenueService venueService)
        {
            foreach (var element in course.Venues.ToList())
            {
                if (!Ids.Contains(element.Venue.Id))
                {
                    teacherTrainingService.RemoveCourseVenue(element.Course, element.Venue);
                }
            }

            foreach (var id in Ids)
            {
                if (!course.Venues.Any(e => e.Venue.Id == id))
                {
                    teacherTrainingService.AddCourseVenue(course, venueService.GetVenue(id));
                }
            }
        }
    }
}