using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateVenueModel
    {
        public ProfileCreateVenueModel()
        {
            Venue = new VenueSelectorModel(false);
        }

        public VenueSelectorModel Venue { get; private set; }

        public void PopulateEntity(Venue venue)
        {
            if (Venue.Id.HasValue) venue.Id = Venue.Id.Value;
            venue.Name = Venue.Name;
            venue.Description = Venue.Description;
        }
    }
}