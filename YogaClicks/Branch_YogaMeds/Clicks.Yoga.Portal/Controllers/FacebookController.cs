using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Facebook;

namespace Clicks.Yoga.Portal.Controllers
{
    public class FacebookController : YogaController
    {
        public FacebookController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IInvitationService invitationService)
            : base(workUnit, securityContext)
        {
            InvitationService = invitationService;
        }

        private IInvitationService InvitationService { get; set; }

        public ActionResult Init()
        {
            return PartialView(CreateModel());
        }

        public ActionResult Channel()
        {
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetExpires(DateTime.Now.AddYears(1));

            return View(CreateModel());
        }

        public ActionResult Canvas()
        {
            return View(CreateModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Invite(string request, string[] users)
        {
            foreach (var user in users)
            {
                InvitationService.InviteFriendFromFacebook("Facebook", string.Format("{0}_{1}", request, user), user);
            }

            WorkUnit.Commit();

            return new EmptyResult();
        }

        private FacebookModel CreateModel()
        {
            var appId = ConfigurationManager.AppSettings["FacebookAppId"];
            return new FacebookModel(appId);

        }
    }
}