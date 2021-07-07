using System.Collections.Generic;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Community;

namespace Clicks.Yoga.Portal.Controllers
{
    public class CommunityController : YogaController
    {
        public CommunityController(
            IWorkUnit workUnit,
            ISecurityContext securityContext
        )
            : base(workUnit, securityContext)
        {
        }

        public ActionResult Navigation()
        {
            return PartialView();
        }
    }
}