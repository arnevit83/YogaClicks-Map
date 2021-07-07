using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Controllers
{
    public class HomeController : YogaController
    {
        public HomeController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IProfileService profileService)
            : base(workUnit, securityContext)
        {
            ProfileService = profileService;
        }

        private IProfileService ProfileService { get; set; }

        public ActionResult Index()
        {
            if (SecurityContext.Authenticated)
            {
                return RedirectToHome();
            }
            else
            {
                return RedirectToAction("CreateAccount", "Registration", new { destination = Request.QueryString["destination"] });
            }
        }

        // DEPLOYMENT OF YODAMEDS - remove this
        [YogaAuthorize]
        public ActionResult EnableYogaMedsDisplay()
        {
            SecurityContext.ActivateYogaMeds();
            return RedirectToHome();
        }
        // --
    }
}