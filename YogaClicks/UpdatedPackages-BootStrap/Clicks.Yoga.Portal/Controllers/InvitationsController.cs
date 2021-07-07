using System;
using System.Web;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Invitations;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Controllers
{
    public class InvitationsController : YogaController
    {
        public InvitationsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IInvitationService invitationService,
            IAccountService accountService)
            : base(workUnit, securityContext)
        {
            InvitationService = invitationService;
            AccountService = accountService;
        }

        private IInvitationService InvitationService { get; set; }

        private IAccountService AccountService { get; set; }

        public ActionResult Redirect(string identifier, string destination)
        {
            Guid guid;

            if (Guid.TryParse(identifier, out guid))
            {
                Response.Cookies.Add(new HttpCookie("Invitation", identifier));
            }

            destination = WebUtility.SanitiseLocalUri(destination, Request.Url);

            return View(new InvitationRedirectModel(destination));
        }

        
        public ActionResult InviteFriendsByEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InviteFriendsByEmail(InvitationInviteFriendsByEmailModel model)
        {
            if (ModelState.IsValid)
            {
                if (AccountService.UsernameExists(model.EmailAddress))
                {
                    ModelState.AddModelError("", Resources.Errors.Invitations.AlreadyRegistered);
                }
            }

            if (ModelState.IsValid)
            {
                InvitationService.InviteFriendByEmail(model.EmailAddress);
                WorkUnit.Commit();

                return View("InviteFriendsByEmailComplete");
            }

            return View(model);
        }
    }
}