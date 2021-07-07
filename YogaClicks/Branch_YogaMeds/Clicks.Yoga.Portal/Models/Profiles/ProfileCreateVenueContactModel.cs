using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateVenueContactModel
    {
        public ProfileCreateVenueContactModel()
        {
            Websites = new WebsiteCollectionEditorModel();
        }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteCollectionEditorModel Websites { get; set; }

        public void PopulateEntity(Venue venue, IWebsiteService websiteService)
        {
            venue.Telephone = Telephone;
            venue.EmailAddress = EmailAddress;

            Websites.PopulateCollection(venue.Websites, websiteService);
        }
    }
}