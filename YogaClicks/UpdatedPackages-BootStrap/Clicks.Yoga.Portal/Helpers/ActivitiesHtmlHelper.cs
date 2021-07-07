using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.Context;

namespace Clicks.Yoga.Portal.Helpers
{
    public static class ActivitiesHtmlHelper
    {
        public static MvcHtmlString EnquireOrFriendRequest(this HtmlHelper helper, ActivityClassPartialModel model, CurrentProfileModel profile)
        {
            var result = helper.Action("SendButton", "Messaging", new { EntityId = model.Repeat.Activity.PromoterHandle.Id, model.Repeat.Activity.PromoterHandle.EntityTypeName, Text = "Enquire" });

            if (result == null || result.ToString().Length == 0)
            {
                result = helper.Action("Button", "Friends", new {ProfileId = model.Repeat.Activity.PromoterHandle.EntityId, ProfileName = model.Repeat.Activity.PromoterHandle.Name});
            }

            return result;
        }
    }
}