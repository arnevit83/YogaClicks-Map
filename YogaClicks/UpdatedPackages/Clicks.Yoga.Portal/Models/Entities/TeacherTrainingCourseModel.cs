using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Medic;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherTrainingCourseModel : EntityModel<TeacherTrainingCourse>, ISecurable
    {
        public TeacherTrainingCourseModel(TeacherTrainingCourse entity) : base(entity) {}

        public int? OwnerId { get; set; }

        public TeacherTrainingOrganisationModel Organisation { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string Duration { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool Finished { get; set; }

        public string PreRequisites { get; set; }

        public string Accreditation { get; set; }

        public string ImageCourtesyOf { get; set; }

        public virtual WebsiteModel Website { get; set; }

        public virtual ImageModel Image { get; set; }

        public virtual StyleModel Style { get; set; }

        public virtual IList<AccreditationBodyModel> AccreditationBodies { get; set; }

        public virtual IList<TeacherTrainingCourseTeacherModel> Teachers { get; set; }

        public virtual IList<TeacherTrainingCourseVenueModel> Venues { get; set; }

        public IList<ConditionModel> Conditions { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public override void Populate(TeacherTrainingCourse entity)
        {
            OwnerId = entity.Organisation.Owner != null ? entity.Organisation.Owner.Id : (int?)null;
            Organisation = new TeacherTrainingOrganisationModel(entity.Organisation);
            Name = entity.Name;
            Notes = entity.Notes;
            Duration = entity.Duration;
            StartDate = entity.StartDate;
            FinishDate = entity.FinishDate;
            Finished = entity.Finished;
            PreRequisites = entity.PreRequisites;
            Accreditation = entity.Accreditation;
            ImageCourtesyOf = entity.ImageCourtesyOf;
            Description = entity.Description;
            Price = entity.Price;

            if (entity.Website != null)
                Website = new WebsiteModel(entity.Website);

            if (entity.Image != null)
                Image = new ImageModel(entity.Image);

            if (entity.Style != null)
                Style = new StyleModel(entity.Style);

            AccreditationBodies = new List<AccreditationBodyModel>();

            foreach (var body in entity.AccreditationBodies)
                AccreditationBodies.Add(new AccreditationBodyModel(body));

            Teachers = new List<TeacherTrainingCourseTeacherModel>();

            foreach (var teacher in entity.Teachers)
                Teachers.Add(new TeacherTrainingCourseTeacherModel(teacher));

            Venues = new List<TeacherTrainingCourseVenueModel>();

            foreach (var venue in entity.Venues)
                Venues.Add(new TeacherTrainingCourseVenueModel(venue));

            Conditions = new List<ConditionModel>();
            foreach (var condition in entity.Conditions)
                Conditions.Add(new ConditionModel(condition));
        }
    }
}