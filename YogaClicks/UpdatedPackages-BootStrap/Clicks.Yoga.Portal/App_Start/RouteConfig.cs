using System.Web.Mvc;
using System.Web.Routing;

namespace Clicks.Yoga.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Lingo",
                "lingo/{action}",
                new { controller = "Glossary", action = "Index" });

            routes.MapRoute(
                "Register",
                "register/{action}",
                new { controller = "Registration", action = "CreateAccount" });

            routes.MapRoute(
                "PoseCategory",
                "poses/categories/{id}",
                new { controller = "Poses", action = "Category" });

            routes.MapRoute(
                "StyleTraits",
                "styles/index/{traitIdString}/{traitNameString}",
                new { controller = "Styles", action = "Index" });

            routes.MapRoute(
                "Index",
                "{controller}/{action}",
                null,
                new { action = "^(Index|Directory|Search)$" });
            
            routes.MapRoute(
                "SingleWithSlug",
                "{controller}/{slug}/{id}/{action}",
                new { action = "Display" },
                new { id = "\\d+" });

            routes.MapRoute(
                "SingleWithoutSlug",
                "{controller}/{id}/{action}",
                new { action = "Display" },
                new { id = "\\d+" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" });
        }
    }
}