using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationLocationUpdateModel
    {
           public OrganisationLocationUpdateModel()
        {
        }

           public OrganisationLocationUpdateModel(TeacherTrainingOrganisation entity, IAddressService addressService)
        {
            EntityId = entity.Id;
            Address = new AddressEditorModel(entity.Address, addressService.GetCountries());
        }

        public int EntityId { get; set; }

        public AddressEditorModel Address { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
             IAddressService addressService)
        {
            entity.Address = Address.PopulateEntity(entity.Address, addressService);
        }
    }
}