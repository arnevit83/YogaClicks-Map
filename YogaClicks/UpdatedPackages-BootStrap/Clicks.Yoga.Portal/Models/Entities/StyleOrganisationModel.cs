using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class StyleOrganisationModel : PrincipalEntityModel<StyleOrganisation>
    {
        public StyleOrganisationModel(StyleOrganisation entity) : base(entity) {}

        public string Name { get; set; }

        public virtual StyleModel Style { get; set; }

        public string Founder { get; set; }

        public int FoundedYear { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public virtual WebsiteModel Website { get; set; }

        public virtual LocationModel Location { get; set; }

        public string Description { get; set; }

        public string Affiliations { get; set; }

        public virtual ImageModel Image { get; set; }

        public virtual ImageModel ProfileBanner { get; set; }

        public override void Populate(StyleOrganisation entity)
        {
            Name = entity.Name;
            Style = new StyleModel(entity.Style);
            Founder = entity.Founder;
            FoundedYear = entity.FoundedYear;
            EmailAddress = entity.EmailAddress;
            Telephone = entity.Telephone;
            Website = new WebsiteModel(entity.Website);
            Location = new LocationModel(entity.Location);
            Description = entity.Description;
            Affiliations = entity.Affiliations;
            Image = new ImageModel(entity.Image);
            ProfileBanner = new ImageModel(entity.ProfileBanner);
        }
    }
}