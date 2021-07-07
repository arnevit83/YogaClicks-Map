using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueObtainStep1Model
    {
        public VenueObtainStep1Model()
        {
            Venue = new VenueSelectorModel();
        }

        public VenueObtainStep1Model(bool showBackButton) : this()
        {
            ShowBackButton = showBackButton;
        }

        public bool ShowBackButton { get; set; }

        public VenueSelectorModel Venue { get; private set; }
    }
}