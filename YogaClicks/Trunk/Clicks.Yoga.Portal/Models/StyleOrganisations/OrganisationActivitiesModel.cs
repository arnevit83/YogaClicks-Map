using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    public class OrganisationActivitiesModel
    {
        public OrganisationActivitiesModel(StyleOrganisation organisation)
        {
            Organisation = new StyleOrganisationModel(organisation);
        }

        public StyleOrganisationModel Organisation { get; private set; }
    }
}