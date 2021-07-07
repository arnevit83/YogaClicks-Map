using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class MediaResourceModel : EntityModel<MediaResource>
    {
        public MediaResourceModel(MediaResource entity) : base(entity) { }

        public MediaResourceTypeModel Type { get; set; }

        public string Uri { get; set; }

        public string Identifier { get; set; }

        public int? EntityId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public ImageModel Image { get; set; }

        public override void Populate(MediaResource entity)
        {
            Type = new MediaResourceTypeModel(entity.Type);
            Uri = entity.Uri;
            Identifier = entity.Identifier;
            EntityId = entity.EntityId;
            Title = entity.Title;
            Content = entity.Content;
            Image = new ImageModel(entity.Image);
        }
    }
}