using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    public class OrganisationNavigationModel
    {
        public OrganisationNavigationModel(IEnumerable<StyleOrganisation> organisations, IEnumerable<Style> styles)
        {
            Organisations = new List<EntityListModel>();

            foreach (var organisation in organisations.OrderBy(x => x.Name))
            {
                Organisations.Add(new EntityListModel(organisation));
            }

            Styles = new List<EntityListModel>();

            foreach (var style in styles.OrderBy(x => x.Name))
            {
                Styles.Add(new EntityListModel(style));
            }
        }

        public IList<EntityListModel> Organisations { get; private set; }

        public IList<EntityListModel> Styles { get; private set; }
    }
}