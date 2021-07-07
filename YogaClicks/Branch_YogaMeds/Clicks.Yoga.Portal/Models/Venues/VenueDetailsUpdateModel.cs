using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueDetailsUpdateModel
    {
         public VenueDetailsUpdateModel()
        {
            Websites = new WebsiteCollectionEditorModel();
            Type = new VenueTypeSelectorModel();
        }

         public VenueDetailsUpdateModel(Venue venue)
        {
            Name = venue.Name;
            VenueId = venue.Id;
            Telephone = venue.Telephone;
            EmailAddress = venue.EmailAddress;
            Type = new VenueTypeSelectorModel(venue.Type);
            Websites = new WebsiteCollectionEditorModel(venue.Websites);
        }

        public void PopulateEntity(
            Venue entity,
            IVenueService venueService,
            IWebsiteService websiteService)
        {
            entity.Name = Name;
            entity.Telephone = Telephone;
            entity.EmailAddress = EmailAddress;
            entity.Type = Type.Selected ? venueService.GetVenueType(Type.Id) : null;
            Websites.PopulateCollection(entity.Websites, websiteService);
        }

        public int VenueId { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteCollectionEditorModel Websites { get; private set; }

        public VenueTypeSelectorModel Type { get; private set; }

        public VenueDetailsUpdateModel PopulateMetadata(
            Venue venue,
            IVenueService venueService)
        {
            Type.PopulateMetadata(venueService.GetVenueTypes());
            return this;
        }
        
    }
}