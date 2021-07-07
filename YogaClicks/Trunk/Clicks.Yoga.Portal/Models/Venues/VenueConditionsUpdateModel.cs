using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueConditionsUpdateModel
    {
        public VenueConditionsUpdateModel() 
        {
            Conditions = new ConditionChooserModel();
        }

        public VenueConditionsUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            Conditions = new ConditionChooserModel(venue.Conditions);
        }

        public void PopulateEntity(
           Venue entity,
            IMedicService medicService)
        {
            Conditions.PopulateCollection(entity.Conditions, medicService);
        }

        public int VenueId { get; set; }

        public ConditionChooserModel Conditions { get; private set; }

        public VenueConditionsUpdateModel PopulateMetadata(
            Venue venue,
            IMedicService medicService)
        {
            Conditions.PopulateMetadata(medicService.GetConditions());
            return this;
        }
    }
}