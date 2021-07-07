using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class VenueModel : PrincipalEntityModel<Venue>
    {
        public VenueModel() {}

        public VenueModel(Venue entity) : base(entity) {}

        public string Name { get; set; }

        public string Description { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public VenueTypeModel Type { get; set; }

        public AddressModel Address { get; set; }

        public IList<WebsiteModel> Websites { get; set; }

        public ImageModel Image { get; set; }

        public ImageModel ProfileBanner { get; set; }

        public IList<StyleModel> Styles { get; private set; }

        public IList<VenueServiceModel> Services { get; private set; }

        public IList<VenueAmenityModel> Amenities { get; private set; } 

        public override void Populate(Venue entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            EmailAddress = entity.EmailAddress;
            Telephone = entity.Telephone;
            
            if (entity.Type != null) Type = new VenueTypeModel(entity.Type);
            if (entity.Address != null) Address = new AddressModel(entity.Address);
            if (entity.Image != null) Image = new ImageModel(entity.Image);
            if (entity.ProfileBanner != null) ProfileBanner = new ImageModel(entity.ProfileBanner);

            Websites = new List<WebsiteModel>();
            Styles = new List<StyleModel>();
            Services = new List<VenueServiceModel>();
            Amenities = new List<VenueAmenityModel>();

            foreach (var website in entity.Websites)
            {
                Websites.Add(new WebsiteModel(website));
            }

            foreach (var style in entity.Styles)
            {
                Styles.Add(new StyleModel(style));
            }

            foreach (var service in entity.Services)
            {
                Services.Add(new VenueServiceModel(service));
            }

            foreach (var amenity in entity.Amenities)
            {
                Amenities.Add(new VenueAmenityModel(amenity));
            }
        }
    }
}