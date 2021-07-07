using System.Collections.Generic;
using System.Data.Spatial;

namespace Clicks.Yoga.Domain.Entities
{
    public class Style : PrincipalEntity, IFanable, INamed, ISearchable
    {
        public Style()
        {
            Traits = new List<StyleTrait>();
        }

        public string Name { get; set; }

        public string Founder { get; set; }

        public string FoundingTime { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image DirectoryImage { get; set; }

        public string ImageCourtesyOf { get; set; }

        public virtual Website Website { get; set; }

        public virtual ICollection<StyleTrait> Traits { get; set; }

        public bool IsSearchable
        {
            get { return true; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Image = Image;
            record.Description = Description;

            if (record.Description != null && record.Description.Length > 150)
                record.Description = record.Description.Substring(0, 150) + "...";


        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }

        public string Video { get; set; }
        public string Intro { get; set; }
    }
}