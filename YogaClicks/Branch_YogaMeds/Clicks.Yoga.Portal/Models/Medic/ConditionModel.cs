using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionModel : EntityModel<Condition>
    {
        public ConditionModel() { }

        public ConditionModel(Condition entity) : base(entity) { }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public virtual ImageModel DirectoryImage { get; set; }

        public virtual ImageModel ProfileBanner { get; set; }

        public string ImageCourtesyOf { get; set; }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(Name); }
        }

        public override void Populate(Condition entity)
        {
            Id = entity.Id;
            Name = entity.Name.Replace(Environment.NewLine, string.Empty);
            Description = entity.Description;
            ImageCourtesyOf = entity.ImageCourtesyOf;
            MetaDescription = entity.MetaDescription;
            MetaTitle = entity.MetaTitle;

            if (entity.ProfileBanner != null) ProfileBanner = new ImageModel(entity.ProfileBanner);
            if (entity.DirectoryImage != null) DirectoryImage = new ImageModel(entity.DirectoryImage);
        }
    }
}