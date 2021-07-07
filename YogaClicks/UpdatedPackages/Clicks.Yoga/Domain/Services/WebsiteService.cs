using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class WebsiteService : ServiceBase, IWebsiteService
    {
        public WebsiteService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public void DeleteWebsite(Website website)
        {
            EntityContext.Websites.Remove(website);
        }
    }
}