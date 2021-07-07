using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    public class CourseUpdateModel : ISecurable
    {
        public CourseUpdateModel()
        {
            StartDate = new CreateCourseDateEditorModel();
            FinishDate = new CreateCourseDateEditorModel();
            AccreditationBodies = new CreateCourseAccreditationBodyChooserModel();
            Website = new WebsiteEditorModel();
            Style = new StyleSelectorModel();
            Teachers = new TeacherTrainingCourseTeacherChooserModel();
            Venues = new TeacherTrainingCourseVenueChooserModel();
            Conditions = new SignUpConditionChooserModel();
            StartDate.Minimum = DateTime.Today;
            StartDate.MaximumYear = DateTime.Today.Year + 5;

            FinishDate.Minimum = StartDate.Minimum;
            FinishDate.Maximum = StartDate.Maximum;
        }

        public CourseUpdateModel(TeacherTrainingCourse entity)
        
        {
            Conditions = new SignUpConditionChooserModel();
            EntityId = entity.Id;
            EntityTypeName = entity.GetEntityTypeName();
            OwnerId = entity.Owner != null ? entity.Owner.Id : (int?)null;
            OrganisationId = entity.Organisation.Id;
            Name = entity.Name;
            Duration = entity.Duration;
            StartDate = new CreateCourseDateEditorModel(entity.StartDate);
            FinishDate = new CreateCourseDateEditorModel(entity.FinishDate);
            PreRequisites = entity.PreRequisites;
            Notes = entity.Notes;
            Accreditation = entity.Accreditation;
            AccreditationBodies = new CreateCourseAccreditationBodyChooserModel(entity.AccreditationBodies);
            Website = new WebsiteEditorModel(entity.Website);
            Style = new StyleSelectorModel(entity.Style);
            Teachers = new TeacherTrainingCourseTeacherChooserModel(entity.Teachers.Select(e => e.Teacher).ToList());
            Venues = new TeacherTrainingCourseVenueChooserModel(entity.Venues.Select(e => e.Venue).ToList());
            Description = entity.Description;
            Price = entity.Price;

            StartDate.Minimum = entity.StartDate.HasValue ? entity.StartDate.Value : DateTime.Today;
            StartDate.MaximumYear = DateTime.Today.Year + 10;

            FinishDate.Minimum = StartDate.Minimum;
            FinishDate.Maximum = StartDate.Maximum;
        }

        public int EntityId { get; set; }
        
        public string EntityTypeName { get; set; }

        public int OrganisationId { get; set; }

        public string Name { get; set; }

        public string Duration { get; set; }

        public CreateCourseDateEditorModel StartDate { get; private set; }

        public CreateCourseDateEditorModel FinishDate { get; private set; }

        public string PreRequisites { get; set; }

        public string Notes { get; set; }

        public string Accreditation { get; set; }

        public CreateCourseAccreditationBodyChooserModel AccreditationBodies { get; private set; }

        public WebsiteEditorModel Website { get; private set; }

        public StyleSelectorModel Style { get; private set; }

        public TeacherTrainingCourseTeacherChooserModel Teachers { get; private set; }

        [DataMember]
        public SignUpConditionChooserModel Conditions { get; set; }

        public TeacherTrainingCourseVenueChooserModel Venues { get; private set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public CourseUpdateModel PopulateMetadata(
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService, IMedicService medicService, ICollection<Condition> condistions)
        {
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            Style.PopulateMetadata(styleService.GetStyles());



            Conditions = new SignUpConditionChooserModel(condistions);

            Conditions.PopulateMetadata(medicService.GetConditions());





            return this;
        }

        public void PopulateEntity(
            TeacherTrainingCourse entity,
            ITeacherTrainingService teacherTrainingService,
            IWebsiteService websiteService,
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService,
            ITeacherService teacherService,
            IVenueService venueService,
            IMedicService medicService)
        {
            entity.Name = Name;
            entity.Duration = Duration;
            entity.StartDate = StartDate.HasValue ? StartDate.DateTime : null;
            entity.FinishDate = FinishDate.HasValue ? FinishDate.DateTime : null;
            entity.PreRequisites = PreRequisites;
            entity.Notes = Notes;
            entity.Accreditation = Accreditation;
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Style = Style.Selected ? styleService.GetStyle(Style.Id) : null;
            entity.Description = Description;
            entity.Price = Price;

            AccreditationBodies.PopulateCollection(entity.AccreditationBodies, accreditationBodyService);
            Teachers.PopulateEntity(entity, teacherTrainingService, teacherService);
            Venues.PopulateEntity(entity, teacherTrainingService, venueService);
            Conditions.PopulateCollection(entity.Conditions, medicService);
        }

        public int? OwnerId { get; private set; }
    }
}