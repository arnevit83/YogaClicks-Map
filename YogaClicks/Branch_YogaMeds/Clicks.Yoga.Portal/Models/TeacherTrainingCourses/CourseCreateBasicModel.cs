using System;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Common;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    [DataContract]
    public class CourseCreateBasicModel
    {
        public CourseCreateBasicModel()
        {
            StartDate = new DateEditorModel();
            FinishDate = new DateEditorModel();

            StartDate.Minimum = DateTime.Today;
            StartDate.MaximumYear = DateTime.Today.Year + 5;

            FinishDate.Minimum = StartDate.Minimum;
            FinishDate.MaximumYear = StartDate.MaximumYear;
        }

        public CourseCreateBasicModel(int organisationId) : this() 
        {
            OrganisationId = organisationId;
        }

        [DataMember]
        public int OrganisationId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public WebsiteEditorModel Website { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public DateEditorModel StartDate { get; set; }

        [DataMember]
        public DateEditorModel FinishDate { get; set; }

        public void PopulateEntity(
            TeacherTrainingCourse entity,
            ITeacherTrainingService teacherTrainingService,
            IWebsiteService websiteService)
        {
            entity.Organisation = teacherTrainingService.GetOrganisation(OrganisationId);
            entity.Name = Name;
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Duration = Duration;
            entity.StartDate = StartDate.DateTime;
            entity.FinishDate = FinishDate.DateTime;
            entity.Description = Description;
            entity.Price = Price;
        }
    }
}