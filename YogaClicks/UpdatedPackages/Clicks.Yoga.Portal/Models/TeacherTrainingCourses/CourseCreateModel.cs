using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    [Serializable]
    public class CourseCreateModel
    {
        public CourseCreateBasicModel BasicModel { get; set; }

        public CourseCreateAccreditationModel AccreditationModel { get; set; }

        public CourseCreateTeachersModel TeachersModel { get; set; }

        public CourseCreateVenuesModel VenuesModel { get; set; }

        public CourseCreateNotesModel NotesModel { get; set; }

        public void PopulateEntity(
            TeacherTrainingCourse entity,
            ITeacherTrainingService teacherTrainingService,
            IWebsiteService websiteService,
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService,
            ITeacherService teacherService,
            IVenueService venueService)
        {
            BasicModel.PopulateEntity(entity, teacherTrainingService, websiteService);
            AccreditationModel.PopulateEntity(entity, accreditationBodyService, styleService);
            TeachersModel.PopulateEntity(entity, teacherTrainingService, teacherService);
            VenuesModel.PopulateEntity(entity, teacherTrainingService, venueService);
            NotesModel.PopulateEntity(entity);
        }
    }
}