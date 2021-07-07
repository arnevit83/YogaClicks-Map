using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueObtainStep3Model
    {
        public VenueObtainStep3Model()
        {
            Address = new AddressEditorModel();
        }

        public VenueObtainStep3Model(int? id, string name, bool showBackButton) : this()
        {
            Id = id;
            Name = name;
            ShowBackButton = showBackButton;
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public bool ShowBackButton { get; set; }

        public bool ObtainBack { get; set; }

        public AddressEditorModel Address { get; private set; }

        public VenueObtainStep3Model PopulateMetadata(IAddressService addressService)
        {
            Address.PopulateMetadata(addressService.GetCountries());
            return this;
        }
    }
}