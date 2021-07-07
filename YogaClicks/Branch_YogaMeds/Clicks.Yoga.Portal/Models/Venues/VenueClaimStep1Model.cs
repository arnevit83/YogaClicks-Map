using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    [Serializable]
    public class VenueClaimStep1Model
    {
        public VenueClaimStep1Model() {}

        public VenueClaimStep1Model(Venue venue)
        {
            Name = venue.Name;
        }

        public string Name { get; private set; }

        public bool Owned { get; set; }
    }
}