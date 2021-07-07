using System;
using System.Collections.Generic;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class AccreditationBody : PrincipalEntity, IFanable, INamed, ISearchable
    {
        public AccreditationBody()
        {
            Accreditations = new List<Accreditation>();
            Teachers = new List<TeacherAccreditation>();
        }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public DateTime? DateFounded { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public string Purpose { get; set; }

        public int? MembershipCount { get; set; }

        public string Founder { get; set; }

        public string ImageCourtesyOf { get; set; }

        public string Affiliations { get; set; }

        public virtual Website Website { get; set; }

        public virtual Image Image { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Accreditation> Accreditations { get; set; }

        public virtual ICollection<TeacherAccreditation> Teachers { get; set; }

        public bool IsSearchable
        {
            get { return Active; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Description = Abbreviation + " " + Description;
            record.Image = Image;

            if (Address != null)
            {
                record.City = Address.City;
                record.Area = Address.Area;
                record.Postcode = Address.Postcode;
                if (Address.Country != null) record.Country = Address.Country.EnglishName;
            }

            record.UpdateEntities(new List<IEnumerable<INamed>> { Teachers.Select(t => t.Teacher)});

        }

        public IEnumerable<ISearchable> GetChildSearchables()
        {
            yield break;
        }

        string IEntityHandle.Location
        {
            get { return Address != null ? Address.LocationName : ""; }
        }
    }
}
