using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    public class CreateCourseAllSteps
    {
        public CreateCourseAllSteps()
        {
            AccreditationBodies = new CreateCourseAccreditationBodyChooserModel();
            Style = new StyleSelectorModel();
            Teachers = new TeacherTrainingCourseTeacherChooserModel();
            Venues = new TeacherTrainingCourseVenueChooserModel();
            Conditions = new SignUpConditionChooserModel();
        }

        public CreateCourseAllSteps(Profile profile, Teacher teacher, Venue venue, TeacherTrainingOrganisation tto, IAccreditationBodyService accreditationBodyService, IStyleService styleService, int organisationId, IMedicService medicService)
        {
            OrganisationId = organisationId;
            Conditions = new SignUpConditionChooserModel();

            Conditions.PopulateMetadata(medicService.GetConditions());
            AccreditationBodies = new CreateCourseAccreditationBodyChooserModel();
            Style = new StyleSelectorModel();

            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            Style.PopulateMetadata(styleService.GetStyles());

            StartDate = new CreateCourseDateEditorModel();
            FinishDate = new CreateCourseDateEditorModel();

            StartDate.Minimum = DateTime.Today;
            StartDate.MaximumYear = DateTime.Today.Year + 5;

            FinishDate.Minimum = StartDate.Minimum;
            FinishDate.MaximumYear = StartDate.MaximumYear;

            Teachers = new TeacherTrainingCourseTeacherChooserModel();
            Venues = new TeacherTrainingCourseVenueChooserModel();
        
         
            Profile = new ProfileModel(profile);

            if (teacher != null)
            {
                Teacher = new TeacherModel(teacher);
            }
      

            if (venue != null)
            {
                VenueModel = new VenueModel(venue);
            }

            if (tto != null)
            {
                TTO = new TeacherTrainingOrganisationModel(tto);
            }
        }

        public void PopulateMetadataClone(TeacherTrainingCourse course, IStyleService styleService, IMedicService medicService, ITeacherService teacherService, IVenueService venueService, IAccreditationBodyService accreditationBody)
        {
            Name = course.Name;
            Website = new WebsiteEditorModel(course.Website);
            Duration = course.Duration;
            Price = course.Price;
            if (course.StartDate != null)
            {
                StartDate = new CreateCourseDateEditorModel(course.StartDate) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            }
            if (course.FinishDate != null)
            {
                FinishDate = new CreateCourseDateEditorModel(course.StartDate) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };


            }
            OrganisationId = course.Organisation.Id;
          
          
            Description = course.Description;
            Duplicate = true;

            AccreditationBodies = new CreateCourseAccreditationBodyChooserModel(course.AccreditationBodies);
            AccreditationBodies.PopulateMetadata(accreditationBody.GetAccreditationBodies());


          
           Accreditation = course.Accreditation;


            Style = new StyleSelectorModel(course.Style);
            Style.PopulateMetadata(styleService.GetStyles());




            ICollection<Teacher> x = course.Teachers.Select(teacher => teacherService.GetTeacher(teacher.Teacher.Id)).ToList();

          
            if (x.Count > 0)
            {
                Teachers = new TeacherTrainingCourseTeacherChooserModel(x);
            }




            Conditions = new SignUpConditionChooserModel(course.Conditions);

            Conditions.PopulateMetadata(medicService.GetConditions());







            foreach (var condition in course.Conditions)
            {
                Conditions.SelectedEntities.Add(new NamedSummaryModel<Condition>(condition));
            }









            ICollection<Venue> v = course.Venues.Select(venue => venueService.GetVenue(venue.Venue.Id)).ToList();



            if (v.Count > 0)
            {


                Venues = new TeacherTrainingCourseVenueChooserModel(v);


            }

            PreRequisites = course.PreRequisites;
            Notes = course.Notes;




        }

        public ProfileModel Profile { get; set; }

        public TeacherModel Teacher { get; set; }

        public VenueModel VenueModel { get; set; }

        public TeacherTrainingOrganisationModel TTO { get; set; }

        public bool HasTeacher
        {
            get { return Teacher != null; }
        }

        public bool HasVenue
        {
            get { return VenueModel != null; }
        }

        public bool HasTTO
        {
            get { return TTO != null; }
        }

        [DataMember]
        public string Accreditation { get; set; }

        [DataMember]
        public CreateCourseAccreditationBodyChooserModel AccreditationBodies { get; private set; }

        [DataMember]
        public StyleSelectorModel Style { get; private set; }


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
        public CreateCourseDateEditorModel StartDate { get; set; }

        [DataMember]
        public CreateCourseDateEditorModel FinishDate { get; set; }

        [DataMember]
        public SignUpConditionChooserModel Conditions { get; set; }


        public string PreRequisites { get; set; }

        public bool Duplicate { get; set; }
        public string Notes { get; set; }

        [DataMember]
        public TeacherTrainingCourseTeacherChooserModel Teachers { get; private set; }

        public TeacherTrainingCourseVenueChooserModel Venues { get; private set; }

        public void PopulateEntity(
            TeacherTrainingCourse entity,
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService,
            ITeacherTrainingService teacherTrainingService,
            IWebsiteService websiteService,
            ITeacherService teacherService,
            IVenueService venueService,
            IMedicService medicService)
        {
            entity.Accreditation = Accreditation;
            entity.Style = Style.Selected ? styleService.GetStyle(Style.Id) : null;
            AccreditationBodies.PopulateCollection(entity.AccreditationBodies, accreditationBodyService);

            entity.Organisation = teacherTrainingService.GetOrganisation(OrganisationId);
            entity.Name = Name;
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Duration = Duration;
            entity.StartDate = StartDate.DateTime;
            entity.FinishDate = FinishDate.DateTime;
            entity.Description = Description;
            entity.Price = Price;

            entity.PreRequisites = PreRequisites;
            entity.Notes = Notes;

          
            Teachers.PopulateEntity(entity, teacherTrainingService, teacherService);
            Conditions.PopulateCollection(entity.Conditions, medicService);
            Venues.PopulateEntity(entity, teacherTrainingService, venueService);
        }
    }
}