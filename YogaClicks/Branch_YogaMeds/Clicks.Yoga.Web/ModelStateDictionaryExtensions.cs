using System.Linq;
using System.Web.Mvc;

namespace Clicks.Yoga.Web
{
    public static class ModelStateDictionaryExtensions
    {
        public static bool AnyErrors(this ModelStateDictionary self)
        {
            return self.Values.Any(v => v.Errors.Count > 0);
        }
    }
}