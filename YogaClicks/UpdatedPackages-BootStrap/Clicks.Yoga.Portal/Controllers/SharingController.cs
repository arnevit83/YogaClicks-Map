using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Sharing;

namespace Clicks.Yoga.Portal.Controllers
{
    public class SharingController : YogaController
    {
        public SharingController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            ISharingService sharingService)
            : base(workUnit, securityContext)
        {
            SharingService = sharingService;
        }

        private ISharingService SharingService { get; set; }

        [ChildActionOnly]
        public ActionResult EntityButtons(EntityReference reference)
        {
            return View(new SharingEntityButtonsModel(reference));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        public ActionResult EntityByEmailButton(EntityReference reference, bool wide = false)
        {
            if (!SecurityContext.HasActor)
            {
                return new EmptyResult();
            }

            return View(new SharingEntityByEmailButtonModel(reference, wide));
        }

        
        public ActionResult EntityByEmail(EntityReference reference)
        {
            return View(new SharingEntityByEmailModel(reference));
        }

        [HttpPost]
        public ActionResult EntityByEmail(SharingEntityByEmailModel model)
        {
            if (ModelState.IsValid)
            {
                SharingService.ShareEntityByEmail(model.Reference, model.EmailAddress, model.Message);
                return View("EntityByEmailComplete");
            }

            return View(model);
        }
    }
}