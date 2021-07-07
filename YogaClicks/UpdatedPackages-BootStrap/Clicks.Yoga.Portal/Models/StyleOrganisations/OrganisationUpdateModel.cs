using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    public class OrganisationUpdateModel
    {
        public OrganisationUpdateModel()
        {
            Style = new StyleSelectorModel();
            Website = new WebsiteEditorModel();
            Location = new LocationEditorModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
        }

        public OrganisationUpdateModel(StyleOrganisation entity)
        {
            Name = entity.Name;
            Founder = entity.Founder;
            FoundedYear = entity.FoundedYear;
            EmailAddress = entity.EmailAddress;
            Telephone = entity.Telephone;
            Description = entity.Description;
            Affiliations = entity.Affiliations;

            Style = new StyleSelectorModel(entity.Style);
            Website = new WebsiteEditorModel(entity.Website);
            Location = new LocationEditorModel(entity.Location);
            Image = new ProfileImageEditorModel(entity.Image);
            ProfileBanner = new ProfileBannerEditorModel(entity.ProfileBanner);
        }

        public StyleOrganisationModel Organisation { get; private set; }

        public string Name { get; set; }

        public virtual StyleSelectorModel Style { get; private set; }

        public string Founder { get; set; }

        public int FoundedYear { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public virtual WebsiteEditorModel Website { get; private set; }

        public virtual LocationEditorModel Location { get; private set; }

        public string Description { get; set; }

        public string Affiliations { get; set; }

        public virtual ProfileImageEditorModel Image { get; private set; }

        public virtual ProfileBannerEditorModel ProfileBanner { get; private set; }

        public IEnumerable<int> FoundedYearOptions
        {
            get
            {
                for (int i = 1900; i <= DateTime.Now.Year; i++)
                {
                    yield return i;
                }
            }
        }

        public void PopulateEntity(
            StyleOrganisation entity,
            IImageService imageService,
            IStyleService styleService,
            IWebsiteService websiteService)
        {
            entity.Name = Name;
            entity.Style = styleService.GetStyle(Style.Id);
            entity.Founder = Founder;
            entity.FoundedYear = FoundedYear;
            entity.EmailAddress = EmailAddress;
            entity.Telephone = Telephone;
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Location = Location.PopulateEntity(entity.Location);
            entity.Description = Description;
            entity.Affiliations = Affiliations;
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
        }

        public OrganisationUpdateModel PopulateMetadata(StyleOrganisation organisation, IStyleService styleService)
        {
            Organisation = new StyleOrganisationModel(organisation);
            Style.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}