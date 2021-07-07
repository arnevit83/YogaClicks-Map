using System;
using System.Configuration;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ImageModel : PrincipalEntityModel<Image>
    {
        public ImageModel() {}

        public ImageModel(Image entity) : this(entity, "Profile.jpg") { }

        public ImageModel(Image entity, string defaultName) : base(entity)
        {
            DefaultName = defaultName;
        }

        public ImageModel(string path, Guid? guid, string extension)
        {
            if (!guid.HasValue)
            {
                Exists = false;
                return;
            }

            Exists = true;
            Name = string.Format("{0}.{1}", guid, extension);
            Uri = string.Format("{0}/{1}", path, Name);
        }

        public string Uri { get; set; }

        public string Name { get; set; }

        public string DefaultName { get; set; }

        public string Url
        {
            get {
                var uri = new Uri(ConfigurationManager.AppSettings["Clicks.Yoga.ImageStore.Url"]);
                uri = new Uri(uri, Exists ? "images/yogaimages/" : "defaults/");
                uri = new Uri(uri, Exists ? Uri : DefaultName);

                return uri.ToString();
            }
        }

        public override void Populate(Image entity)
        {
            Id = entity.Id;
            Uri = entity.Uri;
            Name = entity.Name;
        }
    }
}