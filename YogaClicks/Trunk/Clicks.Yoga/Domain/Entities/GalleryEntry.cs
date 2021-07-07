using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class GalleryEntry : Entity
    {
        public virtual Gallery Gallery { get; set; }

        public string Name { get; set; }

        public virtual Image Image { get; set; }

        public bool Promoted { get; set; }
        
        public GalleryEntry Next()
        {
            if (Gallery == null)
                return null;

            var entries = Gallery.Entries.OrderBy(e => e.Id).ToList();
            var index = entries.FindIndex(m => m.Id == this.Id);

            if (index == entries.Count - 1)
                return null;

            return entries[index + 1];
        }

        public GalleryEntry Previous()
        {
            if (Gallery == null)
                return null;

            var entries = Gallery.Entries.OrderBy(e => e.Id).ToList();
            var index = entries.FindIndex(m => m.Id == this.Id);

            if (index == 0)
                return null;

            return entries[index - 1];
        }

        public GalleryEntry First()
        {
            if (Gallery == null)
                return null;

            return Gallery.Entries.OrderBy(e => e.Id).First();
        }

        public GalleryEntry Last()
        {
            if (Gallery == null)
                return null;

            return Gallery.Entries.OrderBy(e => e.Id).Last();
        }
    }
}