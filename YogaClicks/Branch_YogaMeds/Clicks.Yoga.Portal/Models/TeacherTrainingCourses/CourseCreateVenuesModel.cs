using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    [Serializable]
    public class CourseCreateVenuesModel
    {
        public CourseCreateVenuesModel()
        {
            Venues = new TeacherTrainingCourseVenueChooserModel();
        }

        public TeacherTrainingCourseVenueChooserModel Venues { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(TeacherTrainingCourse entity, ITeacherTrainingService teacherTrainingService, IVenueService venueService)
        {
            Venues.PopulateEntity(entity, teacherTrainingService, venueService);
        }
    }
}