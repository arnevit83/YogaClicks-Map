using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationCreateAddressModel
    {
        public OrganisationCreateAddressModel()
        {
            CreateModel = new OrganisationCreateModel();
            Address = new AddressEditorModel();
            Back = false;
        }

        public OrganisationCreateAddressModel(OrganisationCreateModel createModel)
        {
            CreateModel = createModel;
            Address = new AddressEditorModel();
            Back = false;
        }

        public OrganisationCreateModel CreateModel { get; private set; }

        public AddressEditorModel Address { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
            IAccountService accountService,
            IAddressService addressService,
            IWebsiteService websiteService)
        {
            CreateModel.PopulateEntity(entity, accountService, websiteService);
            entity.Address = Address.PopulateEntity(entity.Address, addressService);
        }

        public OrganisationCreateAddressModel PopulateMetadata(
            IAddressService addressService)
        {
            Address.PopulateMetadata(addressService.GetCountries());
            return this;
        }
    }
}