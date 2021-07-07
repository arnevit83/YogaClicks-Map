using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    [DataContract]
    public class OrganisationCreateStep3Model
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Affiliations { get; set; }

        public bool Back { get; set; }

        public void PopulateEntity(StyleOrganisation entity)
        {
            entity.Description = Description;
            entity.Affiliations = Affiliations;
        }
    }
}