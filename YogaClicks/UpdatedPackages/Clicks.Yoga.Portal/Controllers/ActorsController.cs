using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Actors;

namespace Clicks.Yoga.Portal.Controllers
{
    public class ActorsController : YogaController
    {
        public ActorsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext)
            : base(workUnit, securityContext) {}

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        public ActionResult PickerPartial(string prefix, EntityReference current)
        {
            return View(new PickerPartialModel(prefix, SecurityContext as IPortalSecurityContext, current));
        }
    }
}