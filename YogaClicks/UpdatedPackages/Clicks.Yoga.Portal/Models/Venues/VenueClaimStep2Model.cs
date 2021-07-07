using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Venues
{
    [Serializable]
    public class VenueClaimStep2Model
    {
        public VenueClaimStep2Model()
        {
            Websites = new WebsiteCollectionEditorModel();
        }

        public VenueClaimStep2Model(Venue venue)
        {
            Telephone = venue.Telephone;
            EmailAddress = venue.EmailAddress;
            Websites = new WebsiteCollectionEditorModel(venue.Websites);
        }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteCollectionEditorModel Websites { get; set; }

        public bool Back { get; set; }

        public void PopulateEntity(Venue venue, IWebsiteService websiteService)
        {
            venue.Telephone = Telephone;
            venue.EmailAddress = EmailAddress;

            Websites.PopulateCollection(venue.Websites, websiteService);
        }
    }
}