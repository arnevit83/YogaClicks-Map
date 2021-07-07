using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.AccreditationBodies;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Controllers
{
    public class AccreditationBodysController : YogaController
    {
        public AccreditationBodysController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IAccreditationBodyService accreditationService)
            : base(workUnit, securityContext)
        {
            AccreditationBodyService = accreditationService;
        }
        
        private IAccreditationBodyService AccreditationBodyService { get; set; }

        
        public ActionResult Index(string country)
        {
            var bodies = AccreditationBodyService.GetAccreditationBodies();

            return View(new AccreditationIndexModel(bodies, country));
        }

        
        public ActionResult Display(int id)
        {
            var body = AccreditationBodyService.GetAccreditationBody(id);

            foreach (var result in EnsureUrlSlug(body)) return result;

            return View(new AccreditationBodyDisplayModel(body, AccreditationBodyService.GetAccreditationBodies()));
        }
    }
}
