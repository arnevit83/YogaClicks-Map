using System;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    public class CourseUpdateModel : ISecurable
    {
        public CourseUpdateModel()
        {
            StartDate = new DateEditorModel();
            FinishDate = new DateEditorModel();
            AccreditationBodies = new AccreditationBodyChooserModel();
            Website = new WebsiteEditorModel();
            Style = new StyleSelectorModel();
            Teachers = new TeacherTrainingCourseTeacherChooserModel();
            Venues = new TeacherTrainingCourseVenueChooserModel();
            
            StartDate.Minimum = DateTime.Today;
            StartDate.MaximumYear = DateTime.Today.Year + 5;

            FinishDate.Minimum = StartDate.Minimum;
            FinishDate.Maximum = StartDate.Maximum;
        }

        public CourseUpdateModel(TeacherTrainingCourse entity)
        {
            EntityId = entity.Id;
            EntityTypeName = entity.GetEntityTypeName();
            OwnerId = entity.Owner != null ? entity.Owner.Id : (int?)null;
            OrganisationId = entity.Organisation.Id;
            Name = entity.Name;
            Duration = entity.Duration;
            StartDate = new DateEditorModel(entity.StartDate);
            FinishDate = new DateEditorModel(entity.FinishDate);
            PreRequisites = entity.PreRequisites;
            Notes = entity.Notes;
            Accreditation = entity.Accreditation;
            AccreditationBodies = new AccreditationBodyChooserModel(entity.AccreditationBodies);
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

        public DateEditorModel StartDate { get; private set; }

        public DateEditorModel FinishDate { get; private set; }

        public string PreRequisites { get; set; }

        public string Notes { get; set; }

        public string Accreditation { get; set; }

        public AccreditationBodyChooserModel AccreditationBodies { get; private set; }

        public WebsiteEditorModel Website { get; private set; }

        public StyleSelectorModel Style { get; private set; }

        public TeacherTrainingCourseTeacherChooserModel Teachers { get; private set; }

        public TeacherTrainingCourseVenueChooserModel Venues { get; private set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public CourseUpdateModel PopulateMetadata(
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService)
        {
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            Style.PopulateMetadata(styleService.GetStyles());
            return this;
        }

        public void PopulateEntity(
            TeacherTrainingCourse entity,
            ITeacherTrainingService teacherTrainingService,
            IWebsiteService websiteService,
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService,
            ITeacherService teacherService,
            IVenueService venueService)
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
        }

        public int? OwnerId { get; private set; }
    }
}