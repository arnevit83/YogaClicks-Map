using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class StyleModel : EntityModel<Style>
    {
        public StyleModel() {}

        public StyleModel(Style entity) : base(entity) {}

        public string Name { get; set; }
        public string Intro { get; set; }
        public string Video { get; set; }

        public string Founder { get; set; }

        public string FoundingTime { get; set; }

        public string Description { get; set; }

        public ImageModel Image { get; set; }

        public ImageModel DirectoryImage { get; set; }

        public string ImageCourtesyOf { get; set; }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(Name); }
        }

        public override void Populate(Style entity)
        {
            Id = entity.Id;
            Name = entity.Name.Replace(Environment.NewLine, string.Empty);
            Founder = entity.Founder;
            FoundingTime = entity.FoundingTime;
            Description = entity.Description;
            ImageCourtesyOf = entity.ImageCourtesyOf;
            Intro = entity.Intro;
            Video = entity.Video;

            if (entity.Image != null) Image = new ImageModel(entity.Image);
            if (entity.DirectoryImage != null) DirectoryImage = new ImageModel(entity.DirectoryImage);
        }
    }
}