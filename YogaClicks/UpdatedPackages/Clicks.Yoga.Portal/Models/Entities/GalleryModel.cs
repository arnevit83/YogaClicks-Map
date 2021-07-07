using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class GalleryModel : PrincipalEntityModel<Gallery>
    {
        public GalleryModel(Gallery entity) : base(entity) {}

        public string Name { get; private set; }

        public ImageModel Cover { get; private set; }

        public bool Public { get; private set; }

        public bool Open { get; private set; }

        public bool IsNewsfeed { get; private set; }

        public ProfileModel Profile { get; private set; }

        public  ICollection<GalleryEntry> Entries { get; private set; }

        public override void Populate(Gallery entity)
        {
            Name = entity.Name;
            Cover = new ImageModel(entity.Cover);
            Public = entity.Public;
            Open = entity.Open;
            IsNewsfeed = entity.IsNewsfeed;
            Profile = new ProfileModel(entity.Owner.Profile);
            Entries = entity.Entries;

        }
    }
}