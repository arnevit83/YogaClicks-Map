using System.Web.Mvc;
using System.Web.Routing;

namespace Clicks.Yoga.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             "MedicWithSlug",
             "YogaMeds/{slug}/{id}/{action}",
             new { action = "Display", controller = "Medic" },
             new { id = "\\d+" });

            routes.MapRoute(
                "Medic",
                "YogaMeds/{action}",
                new { controller = "Medic", action = "Index" });

            routes.MapRoute(
                "Lingo",
                "lingo/{action}",
                new { controller = "Glossary", action = "Index" });

            routes.MapRoute(
                "Register",
                "register/{action}",
                new { controller = "Registration", action = "CreateAccount" });




            routes.MapRoute(
                "SignUp",
                "signup/{action}/{step}/{id}",
                new {controller = "SignUp", action = "Start", id = 0, step = 0});

            routes.MapRoute(
                "PoseCategory",
                "poses/categories/{id}",
                new { controller = "Poses", action = "Category" });


            routes.MapRoute(
                "StoriesAction",
                "YogaMap",
                new { controller = "YogaMap", action = "index" });

            routes.MapRoute(
                "StoriesAction2",
                "YogaMap/index/{id}/{action}",
                new { controller = "YogaMap"});




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
            "TeacherSearchPage",
            "{teacherslug}/profile/{action}",
            new { controller = "Teachers", action = "Display" });

            routes.MapRoute(
                "SingleWithoutSlug",
                "{controller}/{id}/{action}",
                new { action = "Display" },
                new { id = "\\d+" });

            routes.MapRoute(
                "TeacherSearchPageHome",
                "{teacherslug}",
                new { controller = "Teachers", action = "Display" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" });

        }
    }
}