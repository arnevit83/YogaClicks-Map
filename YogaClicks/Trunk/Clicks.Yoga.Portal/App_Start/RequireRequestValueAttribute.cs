using System.Reflection;
using System.Web.Mvc;

namespace Clicks.Yoga.Portal
{
    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var test = controllerContext.RequestContext.RouteData.Values.ContainsKey(ValueName);
            return test;
            //Prob need to swap to this in MVC 5 or similar 
            //return (controllerContext.HttpContext.Request[ValueName] != null);
        }
        public string ValueName { get; private set; }
    }
}