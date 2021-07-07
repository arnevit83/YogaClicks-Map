using System;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Registration;

namespace Clicks.Yoga.Portal.Controllers
{
    public class RegistrationController : YogaController
    {
        public RegistrationController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IProfileService profileService,
            IRegistrationService registrationService,
            IEmailService emailService,
            IInvitationService invitationService)
            : base(workUnit, securityContext)
        {
            ProfileService = profileService;
            RegistrationService = registrationService;
            EmailService = emailService;
            InvitationService = invitationService;
        }

        private IProfileService ProfileService { get; set; }

        private IRegistrationService RegistrationService { get; set; }

        private IEmailService EmailService { get; set; }

        private IInvitationService InvitationService { get; set; }
        
        public ActionResult CreateAccount(string emailAddress = null, bool professional = false)
        {
            var model = new RegisterAccountModel(professional);

            model.Username = emailAddress;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAccount(RegisterAccountModel model)
        {
            var result = CreateAccountInternal(model);

            if (result is EmptyResult)
            {
                return View("CreateAccountComplete");
            }

            return View();
        }
        
        public ActionResult CreateAccountStandardPartial(bool professional = false)
        {
            return View(new RegisterAccountModel(professional));
        }

        [HttpPost]
        public ActionResult CreateAccountStandardPartial(RegisterAccountModel model)
        {
            return CreateAccountInternal(model);
        }

        public ActionResult CreateAccountPopupPartial(bool professional = false)
        {
            return View(new RegisterAccountModel(professional));
        }

        [HttpPost]
        public ActionResult CreateAccountPopupPartial(RegisterAccountModel model)
        {
            return CreateAccountInternal(model);
        }
        
        public ActionResult CreateAccountComplete()
        {
            return View();
        }
        
        public ActionResult ActivateAccount(string key)
        {
            try
            {
                var user = RegistrationService.ActivateAccount(key);
                WorkUnit.Commit();

                EmailService.SendWelcomeEmail(user);

                return RedirectToAction("profile-professional-guide", "Static", new
                {
                    registered = true
                });
            }
            catch (DuplicateUsernameException)
            {
                ModelState.AddModelError("", Resources.Errors.Registration.UsernameExists);
            }
            catch (RepeatedActionException)
            {
                ModelState.AddModelError("", Resources.Errors.Registration.ActivationRequestCompleted);
            }
            catch (UnknownEntityException)
            {
                ModelState.AddModelError("", Resources.Errors.Registration.ActivationRequestKeyUnknown);
            }

            return View("ActivateAccountError");
        }

        private ActionResult CreateAccountInternal(RegisterAccountModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var profile = new Profile();

            profile.Forename = model.Forename ?? "";
            profile.Surname = model.Surname ?? "";
            profile.EmailAddress = model.Username;
            profile.ThirdPartyOptOut = model.ThirdPartyOptOut;
            profile.Professional = model.Professional;

            var login = new PasswordLogin();

            login.Username = model.Username;
            login.Password = model.Password;

            var user = new User();

            user.EmailAddress = model.Username;

            try
            {
                var invitationCookie = Request.Cookies["Invitation"];
                var invitationIdentifier = invitationCookie != null ? invitationCookie.Value : null;

                RegistrationService.RegisterAccount(profile, user, login);
                InvitationService.IntroduceInvitee(profile, invitationIdentifier);

                WorkUnit.Commit();

                return new EmptyResult();
            }
            catch (AuthenticatedException)
            {
                ModelState.AddModelError("", Resources.Errors.Registration.AlreadyAuthenticated);
            }
            catch (DuplicateUsernameException)
            {
                ModelState.AddModelError("username", Resources.Errors.Registration.UsernameExists);
            }

            return View(model);
        } 
    }
}