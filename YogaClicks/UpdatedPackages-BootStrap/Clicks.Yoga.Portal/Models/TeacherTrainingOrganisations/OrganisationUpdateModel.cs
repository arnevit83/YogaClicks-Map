using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Postal;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationUpdateModel : ISecurable
    {
        public OrganisationUpdateModel()
        {
            Website = new WebsiteEditorModel();
            Address = new AddressEditorModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
            Styles = new StyleChooserModel();
        }

        public OrganisationUpdateModel(TeacherTrainingOrganisation entity, IAddressService addressService, IStyleService styleService)
        {
            EntityId = entity.Id;
            EntityTypeName = entity.GetEntityTypeName();
            OwnerId = entity.Owner != null ? entity.Owner.Id : (int?)null;
            Name = entity.Name;
            Founder = entity.Founder;
            Website = new WebsiteEditorModel(entity.Website);
            Address = new AddressEditorModel(entity.Address, addressService.GetCountries());
            Image = new ProfileImageEditorModel(entity.Image);
            ProfileBanner = new ProfileBannerEditorModel(entity.ProfileBanner);
            Styles = new StyleChooserModel(entity.Styles, styleService.GetStyles());
            EmailAddress = entity.EmailAddress;
            Description = entity.Description;
        }

        public string EntityTypeName { get; set; }

        public int EntityId { get; set; }

        public string Name { get; set; }

        public string Founder { get; set; }

        public string EmailAddress { get; set; }

        public string Description { get; set; }

        public WebsiteEditorModel Website { get; set; }

        public AddressEditorModel Address { get; set; }

        public ProfileImageEditorModel Image { get; set; }

        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public StyleChooserModel Styles { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
            IAddressService addressService,
            IWebsiteService websiteService,
            IImageService imageService,
            IStyleService styleService)
        {
            entity.Name = Name;
            entity.Founder = Founder;
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Address = Address.PopulateEntity(entity.Address, addressService);
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
            entity.EmailAddress = EmailAddress;
            entity.Description = Description;

            Styles.PopulateCollection(entity.Styles, styleService);
        }

        public int? OwnerId { get; private set; }
    }
}