using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Styles;
using Clicks.Yoga.Web;
using System.Configuration;

namespace Clicks.Yoga.Portal.Controllers
{
    public class StylesController : YogaController
    {
        public StylesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IStyleService styleService
        ) : base(workUnit, securityContext)
        {
            StyleService = styleService;
        }

        private IStyleService StyleService { get; set; }
        
        public ActionResult Navigation()
        {
            var type = Session["Styles.Navigation"] ?? StyleNavigationType.Directory;

            switch ((StyleNavigationType)type)
            {
                case StyleNavigationType.Directory:
                    return DirectoryNavigation();
                case StyleNavigationType.Traits:
                    return TraitsNavigation();
                default:
                    return new EmptyResult();
            }
        }
        
        public ActionResult DirectoryNavigation()
        {
            return View("DirectoryNavigation", new StyleDirectoryNavigationModel(StyleService.GetStyles()));
        }
        
        public ActionResult TraitsNavigation()
        {
            var traitIds = (Session["Styles.Traits"] as List<int>) ?? new List<int>();
            var traits = StyleService.GetTraits();
            var styles = traitIds.Count > 0 ? StyleService.GetStylesByTraits(traitIds) : StyleService.GetStyles();
            var groups = traits.Select(e => e.Group).Distinct();

            return View("TraitsNavigation", new StyleTraitsNavigationModel(groups, styles, traitIds));
        }

        public ActionResult Display(int id)
        {
            var styles = StyleService.GetStyles();
            var style = styles.Where(e => e.Id == id).FirstOrDefault();

            if (style == null) throw new UnknownEntityException();

            foreach (var result in EnsureUrlSlug(style)) return result;

            return View(new StyleDisplayModel(style, styles));
        }

        public ActionResult Directory()
        {
            Session["Styles.Navigation"] = StyleNavigationType.Directory;
            Session["Styles.Last"] = Request.Url.ToString();

            return View();
        }

        public ActionResult DirectoryPartial(int skip = 0, int take = -1)
        {
            Session["Styles.Navigation"] = StyleNavigationType.Directory;
            Session["Styles.Last"] = Request.Url.ToString();

            if (take == -1) take = int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.InfiniteScroll.DefaultNumberOfStyles"]);
            
            return View(new StyleIndexModel(StyleService.GetStylesForInfiniteScroll(skip, take)));
        }

        public ActionResult Index(string traitIdString, string traitNameString)
        {
            var traitIds = WebUtility.SplitUrlSlug<int>(traitIdString);
            
            Session["Styles.Navigation"] = StyleNavigationType.Traits;
            Session["Styles.Traits"] = traitIds;
            Session["Styles.Last"] = Request.Url.ToString();

            return View(new StyleTraitsIndexModel(traitIdString, traitNameString));
        }

        public ActionResult StylesPartial(string traitIdString, string traitNameString, int skip = 0, int take = -1)
        {
            if (take == -1) take = int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.InfiniteScroll.DefaultNumberOfStyles"]);
            var traitIds = WebUtility.SplitUrlSlug<int>(traitIdString);
            var styles = traitIds.Count > 0 ? StyleService.GetStylesByTraitsForInfiniteScroll(traitIds, skip, take) : StyleService.GetStylesForInfiniteScroll(skip, take);
            var model = new StyleTraitsModel(styles, traitIdString, traitNameString);

            return View(model);
        }

        public ActionResult SelectTrait(string traitIdString, string traitNameString, int traitId)
        {
            var traitIds = WebUtility.SplitUrlSlug<int>(traitIdString);
            var traitNames = WebUtility.SplitUrlSlug<string>(traitNameString);

            var trait = StyleService.GetTrait(traitId);

            traitIds.Add(traitId);
            traitNames.Add(WebUtility.UrlSlug(trait.Name));

            Session["Styles.Last"] = Request.Url.ToString();

            return RedirectToTraits(traitIds, traitNames);
        }

        public ActionResult UnselectTrait(string traitIdString, string traitNameString, int traitId)
        {
            var traitIds = WebUtility.SplitUrlSlug<int>(traitIdString);
            var traitNames = WebUtility.SplitUrlSlug<string>(traitNameString);

            var trait = StyleService.GetTrait(traitId);

            traitIds.Remove(traitId);
            traitNames.Remove(WebUtility.UrlSlug(trait.Name));

            Session["Styles.Last"] = Request.Url.ToString();

            return RedirectToTraits(traitIds, traitNames);
        }

        private ActionResult RedirectToTraits(List<int> traitIds, List<string> traitNames)
        {
            return Redirect(TraitsUrl(traitIds, traitNames));
        }

        private string TraitsUrl(List<int> traitIds, List<string> traitNames)
        {
            if (traitIds != null && traitIds.Count > 0)
            {
                if (traitNames == null)
                {
                    traitNames = new List<string>();

                    foreach (var id in traitIds)
                    {
                        var trait = StyleService.GetTrait(id);
                        traitNames.Add(trait.Name);
                    }
                }

                traitIds.Sort();
                traitNames.Sort();

                var traitIdString = WebUtility.UrlSlug(traitIds);
                var traitNameString = WebUtility.UrlSlug(traitNames);

                return Url.Action("Index", new { traitIdString, traitNameString });
            }
            else
            {
                return Url.Action("Index");
            }
        }
    }
}