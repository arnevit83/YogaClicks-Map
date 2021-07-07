using System.Web.Mvc;
using System.Web.UI;
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

        public HomeController()
        {
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
    }
}