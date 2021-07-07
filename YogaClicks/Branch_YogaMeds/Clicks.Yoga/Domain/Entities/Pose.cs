using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class Pose : Entity, IFanable, INamed, ISearchable
    {
        public string EnglishName { get; set; }

        public string SanskritName { get; set; }

        public string Roots { get; set; }

        public string Pronounciation { get; set; }

        public string Instructions { get; set; }

        public string Effects { get; set; }

        public string Tips { get; set; }

        public string Indications { get; set; }

        public string Contraindications { get; set; }

        public virtual AbilityLevel AbilityLevel { get; set; }

        public virtual Image Image { get; set; }

        public virtual Video Video { get; set; }

        public virtual PoseCategory Category { get; set; }

        public bool IsSearchable
        {
            get { return true; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.Name = EnglishName;
            record.Image = Image;
            record.Description = SanskritName;
        }

        string IEntityHandle.Name
        {
            get { return EnglishName; }
        }

        User IEntityHandle.Owner
        {
            get { return null; }
        }

        string INamed.Name
        {
            get { return EnglishName; }
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

        bool IEntityHandle.Active
        {
            get { return true; }
        }

        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }
    }
}
