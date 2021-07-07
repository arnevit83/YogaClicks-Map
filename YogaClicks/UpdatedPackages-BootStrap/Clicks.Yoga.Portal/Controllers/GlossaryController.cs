using System.Web.Mvc;
using System.Web.UI;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Glossary;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Controllers
{
    public class GlossaryController : YogaController
    {
        public GlossaryController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IGlossaryService glossaryService
        )
            : base(workUnit, securityContext)
        {
            GlossaryService = glossaryService;
        }

        private IGlossaryService GlossaryService { get; set; }

     
        public ActionResult Index(string term)
        {
            term = !string.IsNullOrWhiteSpace(term) ? WebUtility.UrlSlug(term) : null;
            return View(new GlossaryIndexModel(GlossaryService.GetGlossaryEntries(), term));
        }
    }
}