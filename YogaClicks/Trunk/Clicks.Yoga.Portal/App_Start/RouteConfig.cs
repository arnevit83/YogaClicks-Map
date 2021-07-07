using System.Web.Mvc;
using System.Web.Routing;
using LowercaseRoutesMVC4;

namespace Clicks.Yoga.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase(
             "MedicWithSlug",
             "YogaMeds/{slug}/{id}/{action}",
             new { action = "Display", controller = "Medic" },
             new { id = "\\d+" });

            routes.MapRouteLowercase(
                "Medic",
                "YogaMeds/{action}",
                new { controller = "Medic", action = "Index" });

            routes.MapRouteLowercase(
                "Lingo",
                "lingo/{action}",
                new { controller = "Glossary", action = "Index" });

            routes.MapRouteLowercase(
                "Register",
                "register/{action}",
                new { controller = "Registration", action = "CreateAccount" });

            routes.MapRouteLowercase(
                "PoseCategory",
                "poses/categories/{id}",
                new { controller = "Poses", action = "Category" });

            routes.MapRouteLowercase(
                "StyleTraits",
                "styles/index/{traitIdString}/{traitNameString}",
                new { controller = "Styles", action = "Index" });

            routes.MapRouteLowercase(
                "Index",
                "{controller}/{action}",
                null,
                new { action = "^(Index|Directory|Search)$" });
            
            routes.MapRouteLowercase(
                "SingleWithSlug",
                "{controller}/{slug}/{id}/{action}",
                new { action = "Display" },
                new { id = "\\d+" });

            routes.MapRouteLowercase(
            "TeacherSearchPage",
            "{teacherslug}/profile/{action}",
            new { controller = "Teachers", action = "Display" });

            routes.MapRouteLowercase(
                "SingleWithoutSlug",
                "{controller}/{id}/{action}",
                new { action = "Display" },
                new { id = "\\d+" });

            routes.MapRouteLowercase(
                "TeacherSearchPageHome",
                "{teacherslug}",
                new { controller = "Teachers", action = "Display" });

            routes.MapRouteLowercase(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" });

        }
    }
}