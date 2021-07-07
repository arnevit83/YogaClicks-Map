using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    [DataContract]
    public class OrganisationCreateModel
    {
        [DataMember]
        public OrganisationCreateStep1Model Step1Model { get; set; }

        [DataMember]
        public OrganisationCreateStep2Model Step2Model { get; set; }

        [DataMember]
        public OrganisationCreateStep3Model Step3Model { get; set; }

        public void PopulateEntity(StyleOrganisation entity, IStyleService styleService, IWebsiteService websiteService)
        {
            if (Step1Model != null) Step1Model.PopulateEntity(entity, styleService);
            if (Step2Model != null) Step2Model.PopulateEntity(entity, websiteService);
            if (Step3Model != null) Step3Model.PopulateEntity(entity);
        }
    }
}