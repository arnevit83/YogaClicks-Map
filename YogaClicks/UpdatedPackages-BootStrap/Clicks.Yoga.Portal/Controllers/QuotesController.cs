using System.Web.Mvc;
using System.Web.UI;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Quotes;

namespace Clicks.Yoga.Portal.Controllers
{
    public class QuotesController : YogaController
    {
        public QuotesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IQuoteService quoteService
        )
            : base(workUnit, securityContext)
        {
            QuoteService = quoteService;
        }

        private IQuoteService QuoteService { get; set; }

    
        public ActionResult Index(string author)
        {
            return View(new QuoteIndexModel(QuoteService.GetQuotes(), author));
        }

        public ActionResult Display(int id)
        {
            var quote = QuoteService.GetQuoteForDisplay(id);

            return RedirectToAction("Index", new { quote.Author });
        }
    }
}