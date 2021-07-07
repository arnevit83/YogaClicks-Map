using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class StyleOrganisation : PrincipalEntity, IActor, IFanable, INamed, ISearchable, IProfileBanner
    {
        public string Name { get; set; }

        public virtual Style Style { get; set; }

        public string Founder { get; set; }

        public int FoundedYear { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public virtual Website Website { get; set; }

        public virtual Location Location { get; set; }

        public string Description { get; set; }

        public string Affiliations { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        string IEntityHandle.Location
        {
            get { return Location != null ? Location.Name : null; }
        }

        public bool IsSearchable
        {
            get { return Active; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Description = "";
            record.Image = Image;
            record.Location = Location != null ? Location.Coordinates : null;
            record.City = Location != null ? Location.Name : null;

            record.Styles.Clear();
            record.Styles.Add(Style);

            record.UpdateEntities(new List<IEnumerable<INamed>> { new List<Style>{Style}});
        }

        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }
    }
}