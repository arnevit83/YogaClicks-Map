using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IStyleOrganisationService
    {
        IList<StyleOrganisation> LuckyDip();
        StyleOrganisation GetOrganisationForDisplay(int id);
        StyleOrganisation GetOrganisationForEditing(int id);
        IList<StyleOrganisation> GetOrganisations();
        StyleOrganisation CreateOrgansiation(int ownerId);
    }
}