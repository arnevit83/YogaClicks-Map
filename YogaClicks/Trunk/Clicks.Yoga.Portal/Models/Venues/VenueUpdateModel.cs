using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueUpdateModel
    {
        public VenueUpdateModel()
        {
            Type = new VenueTypeSelectorModel();
            Address = new AddressEditorModel();
            Websites = new WebsiteCollectionEditorModel();
            Styles = new StyleChooserModel();
            Services = new VenueServiceChooserModel();
            Amenities = new VenueAmenityChooserModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
        }

        public VenueUpdateModel(Venue venue)
        {
            Name = venue.Name;
            Description = venue.Description;
            Telephone = venue.Telephone;
            EmailAddress = venue.EmailAddress;
            Type = new VenueTypeSelectorModel(venue.Type);
            Address = new AddressEditorModel(venue.Address);
            Websites = new WebsiteCollectionEditorModel(venue.Websites);
            Styles = new StyleChooserModel(venue.Styles);
            Services = new VenueServiceChooserModel(venue.Services);
            Amenities = new VenueAmenityChooserModel(venue.Amenities);
            Image = new ProfileImageEditorModel(venue.Image);
            ProfileBanner = new ProfileBannerEditorModel(venue.ProfileBanner);
            NewsletterOptOut = venue.NewsletterOptOut;
        }

        public void PopulateEntity(
            Venue entity,
            IAddressService addressService,
            IImageService imageService,
            IStyleService styleService,
            IVenueService venueService,
            IWebsiteService websiteService)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.Telephone = Telephone;
            entity.EmailAddress = EmailAddress;
            entity.Type = Type.Selected ? venueService.GetVenueType(Type.Id) : null;
            entity.Address = Address.PopulateEntity(entity.Address, addressService);
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
            entity.NewsletterOptOut = NewsletterOptOut;

            Websites.PopulateCollection(entity.Websites, websiteService);
            Styles.PopulateCollection(entity.Styles, styleService);
            Services.PopulateCollection(entity.Services, venueService);
            Amenities.PopulateCollection(entity.Amenities, venueService, entity);
        }

        public VenueModel Venue { get; private set; }

        public string Name { get; set; }

        public VenueTypeSelectorModel Type { get; private set; }

        public AddressEditorModel Address { get; set; }

        public WebsiteCollectionEditorModel Websites { get; private set; }

        public string Description { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public StyleChooserModel Styles { get; private set; }

        public VenueServiceChooserModel Services { get; private set; }

        public VenueAmenityChooserModel Amenities { get; private set; }

        public ProfileImageEditorModel Image { get; set; }

        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public bool NewsletterOptOut { get; set; }

        public VenueUpdateModel PopulateMetadata(
            Venue venue,
            IAddressService addressService,
            IStyleService styleService,
            IVenueService venueService)
        {
            Venue = new VenueModel(venue);

            Address.PopulateMetadata(addressService.GetCountries());
            Styles.PopulateMetadata(styleService.GetStyles());
            Services.PopulateMetadata(venueService.GetVenueServices());
            Type.PopulateMetadata(venueService.GetVenueTypes());
            Amenities.PopulateMetadata(venueService.GetVenueAmenities(), venue.IsResidential);

            return this;
        }
    }
}