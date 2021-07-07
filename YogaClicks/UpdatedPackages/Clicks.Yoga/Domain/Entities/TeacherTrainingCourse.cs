using System;
using System.Collections.Generic;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherTrainingCourse : Entity, INamed, IReviewable, ISearchable, ISecurable
    {
        public TeacherTrainingCourse()
        {
            AccreditationBodies = new List<AccreditationBody>();
            Teachers = new List<TeacherTrainingCourseTeacher>();
            Venues = new List<TeacherTrainingCourseVenue>();
            Conditions = new List<Condition>();
        }

        public virtual TeacherTrainingOrganisation Organisation { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string Duration { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool Finished
        {
            get { return FinishDate.HasValue && FinishDate.Value.AddMonths(1) <= DateTime.Now; }
        }

        public string Description { get; set; }

        public string Price { get; set; }

        public string PreRequisites { get; set; }

        public string Accreditation { get; set; }

        public virtual Website Website { get; set; }

        public virtual Image Image { get; set; }

        public virtual Style Style { get; set; }

        public virtual ICollection<AccreditationBody> AccreditationBodies { get; set; }

        public virtual ICollection<TeacherTrainingCourseTeacher> Teachers { get; set; }

        public virtual ICollection<TeacherTrainingCourseVenue> Venues { get; set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public bool Active { get; set; }

        public string ImageCourtesyOf { get; set; }

        public User Owner
        {
            get { return Organisation == null ? null : Organisation.Owner; }
        }

        public IEnumerable<T> FilterReviewDetailTypes<T>(IEnumerable<T> types) where T : IReviewDetailType
        {
            return types;
        }

        public IEnumerable<Tuple<string, string>> GetReviewHelperDetails()
        {
            var teachers = Teachers.Where(t => t.Confirmed).Select(t => t.Teacher.Name).ToList();
            var venues = Venues.Where(t => t.Confirmed).Select(v => v.Venue.Name).ToList();

            if (StartDate.HasValue) yield return new Tuple<string, string>("Start", StartDate.Value.ToString("MMMM yyyy"));
            if (teachers.Count > 0) yield return new Tuple<string, string>("Teacher(s)", string.Join(", ", teachers));
            if (StartDate.HasValue) yield return new Tuple<string, string>("End", FinishDate.Value.ToString("MMMM yyyy"));
            if (venues.Count > 0) yield return new Tuple<string, string>("Venue(s)", string.Join(", ", venues));
        }

        public bool IsSearchable
        {
            get { return Active; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Image = Image;
            record.Date = StartDate;

            record.Description = Organisation != null ? Organisation.Name : "";
            record.City = Organisation != null && Organisation.Address != null ? Organisation.Address.City : "";
            record.Area = Organisation != null && Organisation.Address != null ? Organisation.Address.Area : "";
            record.Country = Organisation != null && Organisation.Address != null && Organisation.Address.Country != null ? Organisation.Address.Country.EnglishName : "";
            record.Postcode = "";
            record.Location = Organisation.Address.Location;

            var StyleList = new List<Style>();
            if (Style != null)
            {
                StyleList.Add(Style);
            }

            record.UpdateEntities(new List<IEnumerable<INamed>> { StyleList, Venues.Select(v => v.Venue), Teachers.Select(t => t.Teacher), AccreditationBodies, Conditions });

        }

        public int? OwnerId
        {
            get { return Owner == null ? (int?)null : Owner.Id; }
        }

        public override IEntity GetParentEntity()
        {
            return Organisation;
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

       
        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }
    }
}
