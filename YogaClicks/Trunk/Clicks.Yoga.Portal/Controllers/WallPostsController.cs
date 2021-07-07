using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.WallPosts;

namespace Clicks.Yoga.Portal.Controllers
{
    public class WallPostsController : YogaController
    {
        public WallPostsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IWallService wallService)
            : base(workUnit, securityContext)
        {
            WallService = wallService;
        }

        private IWallService WallService { get; set; }

        public ActionResult Display(int id)
        {
            return View(new WallPostDisplayModel(id));
        }
    }
}