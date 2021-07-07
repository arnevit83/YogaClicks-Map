using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    [DataContract]
    public class OrganisationCreateStep2Model
    {
        public OrganisationCreateStep2Model()
        {
            Location = new LocationEditorModel();
            Website = new WebsiteEditorModel();
        }

        [DataMember]
        public LocationEditorModel Location { get; private set; }

        [DataMember]
        public WebsiteEditorModel Website { get; private set; }

        [DataMember]
        public string Telephone { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        public bool Back { get; set; }

        public void PopulateEntity(StyleOrganisation entity, IWebsiteService websiteService)
        {
            entity.Location = Location.PopulateEntity(entity.Location);
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Telephone = Telephone;
            entity.EmailAddress = EmailAddress;
        }
    }
}