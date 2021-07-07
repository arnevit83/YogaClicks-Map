using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Account;
using Clicks.Yoga.Portal.Models.Shared;
using Clicks.Yoga.Portal.Resources.Errors;


namespace Clicks.Yoga.Portal.Controllers
{
    public class AccountController : YogaController
    {
        public AccountController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IAccountService accountService,
            IProfileService profileService,
            IAuthenticationService authenticationService,
            IYmStoryService storyService,
            IGdrpService gdrpService,
            INotificationService notificationService)
            : base(workUnit, securityContext)
        {
            ProfileService = profileService;
            AccountService = accountService;
            AuthenticationService = authenticationService;
            StoryService = storyService;
            GDRPxService = gdrpService;
            NotificationService = notificationService;
        }


        private IProfileService ProfileService { get; set; }

        private IAccountService AccountService { get; set; }

        private IYmStoryService StoryService { get; set; }

        private IGdrpService GDRPxService { get; set; }

        private IAuthenticationService AuthenticationService { get; set; }

        private INotificationService NotificationService { get; set; }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(string destination)
        {
            if (SecurityContext.Authenticated)
            {
             
                return RedirectInternal(destination);
            }
        
            return View(new AccountLoginModel { Destination = destination });
        }


        [HttpPost]
        public ActionResult Authenticate(AccountLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Validation = "Incorrect email / password combination";
                return PartialView("LoginPartial", model);
            }

            model.Destination = string.IsNullOrEmpty(model.Destination) ? "/" : model.Destination;

            if (SecurityContext.Authenticated)
            {
      
                return PartialView("LoginPartial", model);
            }

            try
            {
                AuthenticationService.AuthenticateWithPassword(model.Username, model.Password, model.Persist);
            }
            catch (AuthenticationException) { }

            WorkUnit.Commit();

            if (!SecurityContext.Authenticated)
            {
                if (AccountService.ResendUserActivation(model.Username))
                {
                    model.Validation =
                        "Your account has not been activated. Check your inbox for the activation email and follow the link provided";
                }
                else
                {
                    model.Validation = "Incorrect email / password combination";
                }
            }
            else
            {
                SecurityContext.CurrentProfile.NumberofLoginIn = SecurityContext.CurrentProfile.NumberofLoginIn +1;
                SecurityContext.CurrentProfile.LastLoggedIn = DateTime.Now;

                SecurityContext.CurrentProfile.HasStory = StoryService.HasStory(SecurityContext.CurrentProfile.Id);

                SecurityContext.CurrentProfile.YogaPro = ProfileService.GetProfessionalEntities().Count > 0;
              
                WorkUnit.Commit();

            }
     


            return PartialView("LoginPartial", model);
        }

        [HttpPost]
        public ActionResult Authenticateblank(AccountLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Validation = "Incorrect email / password combination";
                return PartialView("LoginPartialBlank", model);
            }

            model.Destination = string.IsNullOrEmpty(model.Destination) ? "/" : model.Destination;

            if (SecurityContext.Authenticated)
            {

                return PartialView("LoginPartialBlank", model);
            }

            try
            {
                AuthenticationService.AuthenticateWithPassword(model.Username, model.Password, model.Persist);
            }
            catch (AuthenticationException) { }

            WorkUnit.Commit();

            if (!SecurityContext.Authenticated)
            {
                if (AccountService.ResendUserActivation(model.Username))
                {
                    model.Validation =
                        "Your account has not been activated. Check your inbox for the activation email and follow the link provided";
                }
                else
                {
                    model.Validation = "Incorrect email / password combination";
                }
            }
            else
            {
                SecurityContext.CurrentProfile.NumberofLoginIn = SecurityContext.CurrentProfile.NumberofLoginIn + 1;
                SecurityContext.CurrentProfile.LastLoggedIn = DateTime.Now;

                SecurityContext.CurrentProfile.HasStory = StoryService.HasStory(SecurityContext.CurrentProfile.Id);

                SecurityContext.CurrentProfile.YogaPro = ProfileService.GetProfessionalEntities().Count > 0;

                WorkUnit.Commit();

            }



            return PartialView("LoginPartialBlank", model);
        }




        [HttpPost]
        public ActionResult AuthenticateWithFacebook(string accessToken)
        {
            try
            {
                AuthenticationService.AuthenticateWithFacebook(accessToken);
                WorkUnit.Commit();

                // KLUDGE: If a user has been created as part of the authentication process,
                // its id will be 0 until the work unit has been committed.
                SecurityContext.SetCurrentUser(SecurityContext.CurrentUser, false);
            }
            catch (DuplicateUsernameException)
            {
                return Json(new { Succeeded = false, Error = "You are logged into Facebook with an account unconnected to YogaClicks. Please log into the Facebook account that is connected to your YogaClicks profile." });
            }

            SecurityContext.CurrentProfile.NumberofLoginIn = SecurityContext.CurrentProfile.NumberofLoginIn + 1;
            SecurityContext.CurrentProfile.LastLoggedIn = DateTime.Now;

            SecurityContext.CurrentProfile.YogaPro = ProfileService.GetProfessionalEntities().Count > 0;
            SecurityContext.CurrentProfile.HasStory = StoryService.HasStory(SecurityContext.CurrentProfile.Id);
            WorkUnit.Commit();


            return Json(new { Succeeded = true });
        }

 

        public ActionResult Logout()
        {
            SecurityContext.CurrentProfile.HasStory = StoryService.HasStory(SecurityContext.CurrentProfile.Id);
            WorkUnit.Commit();
            SecurityContext.SetCurrentUser(null, false);

            Session["SuperUser"] = null;

      

           


            return RedirectToAction("Index", "Home");
        }


        public ActionResult PasswordResetRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordResetRequest(AccountPasswordResetRequestModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                AuthenticationService.RequestPasswordReset(model.Username);
                WorkUnit.Commit();
                return View("PasswordResetRequestComplete", model);
            }
            catch (UnknownIdentifierException)
            {
                ModelState.AddModelError("Username", Resources.Errors.Authentication.UsernameUnknown);
            }

            return View(model);
        }


        public ActionResult PasswordReset(string key)
        {
            ModelState.Clear();

            try
            {
                AuthenticationService.ValidatePasswordResetRequest(key);
                WorkUnit.Commit();
                return View(new AccountPasswordResetModel(key));
            }
            catch (UnknownIdentifierException)
            {
                ModelState.AddModelError("Key", Resources.Errors.Authentication.PasswordResetRequestUnknown);
            }
            catch (RepeatedActionException)
            {
                ModelState.AddModelError("", Resources.Errors.Authentication.PasswordResetRequestCompleted);
            }
            catch (OpportunityExpiredException)
            {
                ModelState.AddModelError("", Resources.Errors.Authentication.PasswordResetRequestExpired);
            }

            return View("PasswordResetError", new AccountPasswordResetModel(key));
        }

        [HttpPost]
        public ActionResult PasswordReset(AccountPasswordResetModel model)
        {
            if (!ModelState.IsValid) return View(model);

            bool succeeded = false;

            try
            {
                AuthenticationService.ResetPassword(model.Key, model.Password);
                WorkUnit.Commit();
                succeeded = true;
            }
            catch (UnknownIdentifierException)
            {
                ModelState.AddModelError("Key", Resources.Errors.Authentication.PasswordResetRequestUnknown);
            }
            catch (RepeatedActionException)
            {
                ModelState.AddModelError("", Resources.Errors.Authentication.PasswordResetRequestCompleted);
            }
            catch (OpportunityExpiredException)
            {
                ModelState.AddModelError("", Resources.Errors.Authentication.PasswordResetRequestExpired);
            }

            if (succeeded)
            {
                return View("PasswordResetComplete");
            }
            else
            {
                return View(model);
            }
        }


        public ActionResult PasswordResetError()
        {
            ModelState.AddModelError("", "This is a test error.");
            return View();
        }


        public ActionResult PasswordResetComplete()
        {
            return View();
        }


        [YogaAuthorize]
        public ActionResult ConfirmEmailAddressChange(string key)
        {
            try
            {
                AccountService.ConfirmEmailAddressChange(key);
                WorkUnit.Commit();
            }
            catch (UnknownIdentifierException)
            {
                ModelState.AddModelError("Key", Resources.Errors.Account.EmailAddressChangeRequestUnknown);
            }
            catch (RepeatedActionException)
            {
                ModelState.AddModelError("", Resources.Errors.Account.EmailAddressChangeRequestCompleted);
            }
            catch (OpportunityExpiredException)
            {
                ModelState.AddModelError("", Resources.Errors.Account.EmailAddressChangeRequestExpired);
            }
            catch (DuplicateUsernameException)
            {
                ModelState.AddModelError("", Resources.Errors.Account.EmailAddressChangeRequestDuplicate);
            }

            return View();
        }


        [YogaAuthorize]
        public ActionResult Settings()
        {
            NotificationService.EnsureNotificationPreferencesExist(SecurityContext.CurrentUser);
            var model = new AccountSettingsModel(SecurityContext.CurrentUser, NotificationService);
            model.PopuplateMetadata(AccountService, NotificationService);
            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Settings(AccountSettingsModel model)
        {
            if (!ModelState.IsValid) return View("Settings", model.PopuplateMetadata(AccountService, NotificationService));

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                try
                {
                    if (AccountService.CurrentUserHasFederatedLogin("Facebook"))
                    {
                        AccountService.RemoveFederatedLogin(SecurityContext.CurrentUser, "Facebook");

                        if (AccountService.CurrentUserHasPasswordLogin())
                        {
                            AccountService.SetPassword(SecurityContext.CurrentUser, model.NewPassword);
                        }
                        else
                        {
                            var login = new PasswordLogin();

                            login.Password = model.NewPassword;
                            login.User = SecurityContext.CurrentUser;
                            login.Username = SecurityContext.CurrentUser.EmailAddress;

                            AccountService.AddPasswordLogin(login);
                        }
                    }
                    else
                    {
                        AccountService.ChangePassword(SecurityContext.CurrentUser, model.CurrentPassword, model.NewPassword);
                    }
                }
                catch (AuthenticationException)
                {
                    ModelState.AddModelError("CurrentPassword", Resources.Errors.Account.PasswordIncorrect);
                    return View("Settings", model.PopuplateMetadata(AccountService, NotificationService));
                }
                catch (UnacceptablePasswordException)
                {
                    ModelState.AddModelError("", Resources.Errors.Account.PasswordUnacceptable);
                    return View("Settings", model.PopuplateMetadata(AccountService, NotificationService));
                }
                catch (DuplicateUsernameException)
                {
                    ModelState.AddModelError("", Resources.Errors.Account.UsernameExists);
                    return View("Settings", model.PopuplateMetadata(AccountService, NotificationService));
                }
            }

            if (!string.IsNullOrEmpty(model.NewEmailAddress))
            {
                try
                {
                    if (SecurityContext.IsSuperUser())
                    {
                        AccountService.ChangeEmailAddress(SecurityContext.CurrentUser, model.NewEmailAddress);
                    }
                    else
                    {
                        AccountService.RequestEmailAddressChange(SecurityContext.CurrentUser, model.NewEmailAddress);
                    }

                    model.CurrentEmailAddress = model.NewEmailAddress;
                    model.NewEmailAddress = "";
                    model.NewEmailAddressConfirmation = "";

                    ModelState.Clear();

                    if (!SecurityContext.IsSuperUser())
                    {
                        ModelState.AddModelError("", Resources.Errors.Account.EmailConfirmationRequired);
                    }
                }
                catch (DuplicateUsernameException)
                {
                    ModelState.AddModelError("", Resources.Errors.Account.UsernameExists);
                    return View("Settings", model.PopuplateMetadata(AccountService, NotificationService));
                }
            }

            if (!SecurityContext.CurrentProfile.Professional)
            {
                SecurityContext.CurrentProfile.Public = model.ProfilePublic;
            }

            if (SecurityContext.CurrentUser.PrivacyPreferences == null)
            {
                SecurityContext.CurrentUser.PrivacyPreferences = new PrivacyPreferences();
            }

            SecurityContext.CurrentUser.PrivacyPreferences.SharingPermitted = model.SharingPermitted;

            SecurityContext.CurrentProfile.ThirdPartyOptOut = model.ThirdPartyOptOut;

            NotificationService.ToggleNotificationDigest(SecurityContext.CurrentUser, model.NotificationDigestTimescale != null, model.NotificationDigestTimescale);

            foreach (var pair in model.NotificationCategories.Selection)
            {
                NotificationService.ToggleNotificationCategoryEnabled(SecurityContext.CurrentUser, Convert.ToInt32(pair.Key), pair.Value);
            }

            WorkUnit.Commit();

            return View("Settings", model.PopuplateMetadata(AccountService, NotificationService));
        }

        public ActionResult LoginPartial()
        {
            return PartialView(new AccountLoginModel());
        }

        public ActionResult LoginPartialBlank()
        {
            return PartialView(new AccountLoginModel());
        }

        public ActionResult ChangeActor(int id, string typeName)
        {
            AccountService.ChangeActor(id, typeName);

            return Json(((IPortalSecurityContext)SecurityContext).CurrentActor);
        }

        public ActionResult HumansOnly()
        {
            return View();
        }

        [YogaAuthorize]
        public ActionResult Deactivate()
        {
            return View(new AccountDeactivateModel(SecurityContext.CurrentUser));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Deactivate(AccountDeactivateModel model)
        {
            AccountService.Deactivate(SecurityContext.CurrentUser, model.Strategy);

            WorkUnit.Commit();

            var superUser = GetCurrentSuperUser();

            if (superUser != null)
            {
                return RevertSuperUser();
            }

            SecurityContext.SetCurrentUser(null, false);

            return RedirectToAction("Index", "Home");
        }

        [YogaAuthorize]
        public ActionResult AdminDeactivate(int userId)
        {
            var user = AccountService.GetUser(userId);

            AccountService.Deactivate(user, DeactivationStrategy.Delete);

            WorkUnit.Commit();

            return Content("OK");
        }

        [YogaAuthorize]
        public ActionResult SuperUserAccess(int userId, Uri destination)
        {
            if (!SecurityContext.IsSuperUser()) return Redirect(destination.ToString());

            var superUser = Session["SuperUser"];

            if (superUser == null)
                Session["SuperUser"] = SecurityContext.CurrentUser.Id;

            SecurityContext.SetCurrentUser(AccountService.GetUser(userId), false);

            return Redirect(destination.ToString());
        }

        public ActionResult SuperUserAccessButton(int userId)
        {
            var user = AccountService.GetUser(userId);

            return PartialView("SuperUserAccessPartial", new SuperUserAccessPartialModel(userId, user));
        }

        public ActionResult SuperUserWarning()
        {
            if (GetCurrentSuperUser() == null)
                return new EmptyResult();
            
            return PartialView("SuperUserWarningPartial", new SuperUserWarningPartialModel(SecurityContext.CurrentUser));
        }

        [YogaAuthorize]
        public ActionResult RevertSuperUser()
        {
            SecurityContext.SetCurrentUser(AccountService.GetUser((int)Session["SuperUser"]), true);

            Session["SuperUser"] = null;

            var userId = SecurityContext.CurrentUser.Id;

            return RedirectToAction("Display", "Profiles", new { id = userId });
        }

        public User GetCurrentSuperUser()
        {
            var superUser = (int?)Session["SuperUser"];

            if (superUser != null)
            {
                User user = AccountService.GetUser((int) Session["SuperUser"]);

                if (user.IsSuperUser)
                    return user;
            }

            return null;
        }
       

        public ActionResult Thanks(String X )
        {
            string email;
            try
            {
                  email = X.ToLower();
            }
            catch (Exception)
            {
                return View();
            }
         


            var xy = new Gdrp {GDRPEmailAddress = email};
        
            GDRPxService.AddGDPR(xy);
            WorkUnit.Commit();
            return View();
           
        }

    }
}