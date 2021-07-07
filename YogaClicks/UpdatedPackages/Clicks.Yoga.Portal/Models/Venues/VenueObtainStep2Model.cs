namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueObtainStep2Model
    {
        public VenueObtainStep2Model() {}

        public VenueObtainStep2Model(int id, bool showBackButton)
        {
            Id = id;
            ShowBackButton = showBackButton;
        }

        public VenueObtainStep2Model(string name, bool showBackButton)
        {
            Name = name;
            ShowBackButton = showBackButton;
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public bool Owned { get; set; }

        public bool ShowBackButton { get; set; }

        public bool ObtainBack { get; set; }

        public string Email { get; set; }
    }
}
