using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    public class OrganisationGroupsModel
    {
        public OrganisationGroupsModel(StyleOrganisation organisation)
        {
            Organisation = new StyleOrganisationModel(organisation);
        }

        public StyleOrganisationModel Organisation { get; private set; }
    }
}