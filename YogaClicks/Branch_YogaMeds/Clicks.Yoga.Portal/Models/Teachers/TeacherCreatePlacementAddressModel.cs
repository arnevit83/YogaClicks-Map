using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherCreatePlacementAddressModel
    {
        public TeacherCreatePlacementAddressModel()
        {
            Address = new AddressEditorModel();
        }

        public TeacherCreatePlacementAddressModel(string venueName, bool owned, string emailAddress)
            : this()
        {
            VenueName = venueName;
            Owned = owned;
            EmailAddress = emailAddress;
        }

        public string VenueName { get; set; }

        public bool Owned { get; set; }

        public string EmailAddress { get; set; }

        public AddressEditorModel Address { get; set; }

        public TeacherCreatePlacementAddressModel PopulateMetadata(IAddressService addressService)
        {
            Address.PopulateMetadata(addressService.GetCountries());
            return this;
        }
    }
}