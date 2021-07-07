using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clicks.Yoga.Context;

// DEPLOYMENT OF YODAMEDS - remove this class
namespace Clicks.Yoga.Portal
{
    public class YogaMedsAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // TODO: Implement attribute dependency injection.
            var securityContext = DependencyResolver.Current.GetService<ISecurityContext>();

            if (!securityContext.IsYogaMeds()) return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var path = filterContext.HttpContext.Request.Path;
            var query = filterContext.HttpContext.Request.QueryString;
            var uri = "/?destination=" + HttpUtility.UrlEncode(string.Format("{0}?{1}", path, query));
            filterContext.Result = new RedirectResult(uri);
        }

        protected string[] Split(string original)
        {
            if (string.IsNullOrEmpty(original)) return new string[0];
            
            return (
                from piece in original.Split(',')
                let trimmed = piece.Trim()
                where !string.IsNullOrEmpty(trimmed)
                select trimmed
            ).ToArray<string>();

        }
    }
}