using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class AccreditationBodyService : ServiceBase, IAccreditationBodyService
    {
        public AccreditationBodyService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public IList<AccreditationBody> LuckyDip()
        {
            return EntityContext.AccreditationBodies.OrderBy(p => Guid.NewGuid()).Take(5).ToList();
        }

        public System.Collections.Generic.IList<Entities.AccreditationBody> GetAccreditationBodies()
        {
            return EntityContext.AccreditationBodies.OrderBy(a => a.Name).ToList();
        }

        public Entities.AccreditationBody GetAccreditationBody(int id)
        {
            return EntityContext.AccreditationBodies.Get(id);
        }
    }
}