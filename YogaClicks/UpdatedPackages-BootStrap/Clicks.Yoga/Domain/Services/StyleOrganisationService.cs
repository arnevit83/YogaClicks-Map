using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class StyleOrganisationService : ServiceBase, IStyleOrganisationService
    {
        public StyleOrganisationService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public IList<StyleOrganisation> LuckyDip()
        {
            return EntityContext.StyleOrganisations.OrderBy(e => Guid.NewGuid()).Take(5).ToList();
        }

        public StyleOrganisation GetOrganisationForDisplay(int id)
        {
            var organisation = EntityContext.StyleOrganisations.Get(e => e.Id == id);

            if (organisation == null) throw new UnknownEntityException();

            return organisation;
        }

        public StyleOrganisation GetOrganisationForEditing(int id)
        {
            var organisation = GetOrganisationForDisplay(id);

            if (!SecurityContext.CanUpdate(organisation)) throw new PermissionDeniedException();

            return organisation;
        }

        public IList<StyleOrganisation> GetOrganisations()
        {
            return EntityContext.StyleOrganisations.ToList();
        }

        public StyleOrganisation CreateOrgansiation(int ownerId)
        {
            var owner = EntityContext.Users.Get(ownerId);

            if (owner == null)
                throw new ArgumentOutOfRangeException("ownerId");
            if (!SecurityContext.CanUpdate(owner))
                throw new PermissionDeniedException();

            var organisation = new StyleOrganisation { Owner = owner };

            EntityContext.StyleOrganisations.Add(organisation);

            return organisation;
        }
    }
}