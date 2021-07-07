using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherPlacementVenueModel : ISecurable
    {
        public TeacherPlacementVenueModel(Venue venue)
        {
            Id = venue.Id;
            Name = venue.Name;
            Description = venue.Description;
            if (venue.Owner != null) OwnerId = venue.Owner.Id;
            if (venue.Address != null) Address = new AddressModel(venue.Address);
            if (venue.Image != null) Image = new ImageModel(venue.Image);
        }

        public TeacherPlacementVenueModel(VenueModel venue)
        {
            Id = venue.Id;
            Name = venue.Name;
            Description = venue.Description;
            Address = venue.Address;
            Image = venue.Image;

            var securable = (ISecurable)venue;

            OwnerId = securable.OwnerId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public AddressModel Address { get; set; }

        public ImageModel Image { get; set; }

        public int? OwnerId { get; private set; }
    }
}