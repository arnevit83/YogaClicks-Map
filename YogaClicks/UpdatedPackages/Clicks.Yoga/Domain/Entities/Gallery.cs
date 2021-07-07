using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class Gallery : PrincipalEntity, IEntityHandle
    {
        public const string DefaultName = "Untitled album";

        public Gallery()
        {
            Entries = new List<GalleryEntry>();
            Associations = new List<GalleryAssociation>();
        }

        public string Name { get; set; }

        public virtual Image Cover { get; set; }

        public virtual ICollection<GalleryEntry> Entries { get; private set; }



        public bool Public { get; set; }

        public bool Open { get; set; }

        public bool IsNewsfeed { get; set; }

        public virtual ICollection<GalleryAssociation> Associations { get; private set; }

        Image IEntityHandle.Image
        {
            get { return Cover; }
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }
    }
}