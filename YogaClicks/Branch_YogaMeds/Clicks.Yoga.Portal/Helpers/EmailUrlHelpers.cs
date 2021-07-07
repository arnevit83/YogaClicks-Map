using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Clicks.Yoga.Portal.Helpers
{
    public static class EmailUrlHelpers
    {
        public static string EmailAction(this UrlHelper helper, string actionName, string controllerName)
        {
            return helper.EmailAction(actionName, controllerName, new RouteValueDictionary());
        }

        public static string EmailAction(this UrlHelper helper, string actionName, string controllerName, object routeValues)
        {
            return helper.EmailAction(actionName, controllerName, new RouteValueDictionary(routeValues));
        }

        public static string EmailAction(this UrlHelper helper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var url = HttpContext.Current.Request.Url;
            return helper.Action(actionName, controllerName, routeValues, url.Scheme, url.Host);
        }
    }
}