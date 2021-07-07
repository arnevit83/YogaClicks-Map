using Clicks.Yoga.Context;

namespace Clicks.Yoga.Domain.Services
{
    public class ServiceBase
    {
        public ServiceBase(IEntityContext entityContext, ISecurityContext securityContext)
        {
            EntityContext = entityContext;
            SecurityContext = securityContext;
        }

        protected IEntityContext EntityContext { get; private set; }

        protected ISecurityContext SecurityContext { get; private set; }
    }
}