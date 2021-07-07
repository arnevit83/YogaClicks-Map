using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherPlacement : Entity, IRelationship, ISearchable
    {
        public TeacherPlacement() {}

        public TeacherPlacement(Teacher teacher, Venue venue)
        {
            Teacher = teacher;
            Venue = venue;
        }

        public virtual Teacher Teacher { get; set; }

        public virtual Venue Venue { get; set; }

        public bool Confirmed { get; set; }

        IEnumerable<Entity> IRelationship.Targets
        {
            get
            {
                yield return Teacher;
                yield return Venue;
            }
        }

        public bool IsSearchable
        {
            get { return true; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.Name = Teacher.Name;
            record.Description = "";
            record.Stubbed = Confirmed;
            record.OwnerId = Teacher.Id;
            record.Image = Teacher.Image;

            record.Location = Venue.Address.Location;
            record.City = Venue.Address.Location != null ? Venue.Address.City : null;
        }

        public IEnumerable<ISearchable> GetChildSearchables()
        {
            yield break;
        }
    }
}