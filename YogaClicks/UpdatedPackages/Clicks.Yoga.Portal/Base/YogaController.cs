using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Search;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal
{
    public class YogaController : Controller
    {
        public YogaController(IWorkUnit workUnit, ISecurityContext securityContext)
        {
            WorkUnit = workUnit;
            SecurityContext = securityContext;
        }

        public YogaController()
        {
        }

        protected IWorkUnit WorkUnit { get; set; }
        internal ISecurityContext SecurityContext { get; set; }

        protected ActionResult RedirectToHome()
        {
            if (SecurityContext.Authenticated)
            {
                var profile = SecurityContext.CurrentProfile;

                if (!profile.Professional)
                {
                   // return RedirectToAction("News", "Profiles", new { profile.Id });
                }

                var actor = SecurityContext.AvailableActors.FirstOrDefault();

                if (actor != null)
                {
                  //  return RedirectToAction("Display", actor.EntityType.Controller, new { Id = actor.EntityId });
                }

               // return RedirectToAction("how-to-use-this-site", "Static");
                return RedirectToAction("Trending", "static");
            }

            return RedirectToAction("Index", "Home");
        }

        protected ActionResult RedirectInternal(string uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            return Redirect(WebUtility.SanitiseLocalUri(uri, Request.Url));
        }

        protected IEnumerable<ActionResult> EnsureUrlSlug(INamed entity)
        {
            if (Request.IsAjaxRequest()) yield break;

            var slug = (string)RouteData.Values["slug"];
            var correct = WebUtility.UrlSlug(entity.Name);

            if (slug != correct)
            {
                var action = (string)RouteData.Values["action"];
                var values = new RouteValueDictionary(RouteData.Values);

                values["slug"] = correct;

                var url = Url.Action(action, values);

                if (Request.QueryString.Count > 0) url = url + "?" + Request.QueryString.ToString();

                yield return RedirectPermanent(url);
            }
        }

        protected void ChangeActorIfOwner(IActor actor)
        {
            if (SecurityContext.IsOwner(actor))
            {
                SecurityContext.SetCurrentActor(actor);
            }
        }

        protected void SetSearchCriteria(SearchCriteriaModel criteria)
        {
            Session["SearchCriteria"] = criteria;
        }
    }
}