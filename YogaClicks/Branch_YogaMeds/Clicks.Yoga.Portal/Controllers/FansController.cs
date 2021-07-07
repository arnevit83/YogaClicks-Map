using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Fans;

namespace Clicks.Yoga.Portal.Controllers
{
    public class FansController : YogaController
    {
        public FansController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IFanService fanService,
            IEntityService entityService)
            : base(workUnit, securityContext)
        {
            FanService = fanService;
            EntityService = entityService;
        }

        private IFanService FanService { get; set; }

        private IEntityService EntityService { get; set; }

        public ActionResult ListPartial(int entityId, string entityTypeName)
        {
            var fans = FanService.GetFans(entityId, entityTypeName);
            var handle = EntityService.GetEntityHandle(entityId, entityTypeName);
            return View(new FanListPartialModel(handle, fans));
        }

        public ActionResult Button(int entityId, string entityTypeName, bool newsfeed = false)
        {
            bool isFanned = false;
            bool canFan = false;

            if (SecurityContext.Authenticated)
            {
                isFanned = FanService.IsFanned(SecurityContext.CurrentUser, entityId, entityTypeName);
                canFan = FanService.CanFan(SecurityContext.CurrentUser, entityId, entityTypeName);
            }

            var vm = new FanButtonModel(entityId, entityTypeName, isFanned, canFan);
            vm.NewsFeed = newsfeed;
            
            if (newsfeed)
            {
                vm.FanCount = FanService.GetFanCount(entityId, entityTypeName);
            }

            return View(vm);
        }

        public ActionResult Fan(int entityId, string entityTypeName, bool newsfeed = false)
        {
            if (SecurityContext.Authenticated && !SecurityContext.CurrentProfile.Professional)
            {
                FanService.Fan(SecurityContext.CurrentUser, entityId, entityTypeName);
                WorkUnit.Commit();
            }

            var vm = new FanButtonModel(entityId, entityTypeName, true, true);
            vm.NewsFeed = newsfeed;
            if (newsfeed)
            {
                vm.FanCount = FanService.GetFanCount(entityId, entityTypeName);    
            }
            
            return View("Button", vm);
        }

        public ActionResult Unfan(int entityId, string entityTypeName, bool newsfeed = false)
        {
            if (SecurityContext.Authenticated && !SecurityContext.CurrentProfile.Professional)
            {
                FanService.Unfan(SecurityContext.CurrentUser, entityId, entityTypeName);
                WorkUnit.Commit();
            }

            var vm = new FanButtonModel(entityId, entityTypeName, false, true);
            vm.NewsFeed = newsfeed;

            if (newsfeed)
            {
                vm.FanCount = FanService.GetFanCount(entityId, entityTypeName);
            }

            return View("Button", vm);
        }

        public ActionResult Confirmation(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IEntityHandle>(entityId, entityTypeName);
            return View(new FanConfirmationModel(entity));
        }
    }
}
