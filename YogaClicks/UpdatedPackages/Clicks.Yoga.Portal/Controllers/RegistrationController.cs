using System;
using System.IO;
using System.Web;
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
            IAuthenticationService authenticationService,
            IRegistrationService registrationService,
            IEmailService emailService,
            IInvitationService invitationService)
            : base(workUnit, securityContext)
        {
            ProfileService = profileService;
            RegistrationService = registrationService;
            EmailService = emailService;
            InvitationService = invitationService;
            AuthenticationService = authenticationService;
        }

        private IProfileService ProfileService { get; set; }

        private IRegistrationService RegistrationService { get; set; }

        private IEmailService EmailService { get; set; }

        private IInvitationService InvitationService { get; set; }


        private IAuthenticationService AuthenticationService { get; set; }





        public ActionResult CreateAccountHowandWhy(string emailAddress = null, bool professional = false)
        {
            var model = new RegisterAccountModel(professional);

            model.Username = emailAddress;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAccountHowandWhy(RegisterAccountModel model)
        {
            var result = CreateAccountInternal(model);

            if (result is EmptyResult)
            {
                return View("CreateAccountComplete");
            }

            return View();
        }



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

                //return RedirectToAction("profile-professional-guide", "Static", new
                //{
                //    registered = true
                //});

                return RedirectToAction("welcome", "Signup");

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
 

        [HttpGet]
        public ActionResult CsvUpload()
        {

            if (!SecurityContext.IsSuperUser()) Redirect("/");

            return View();
        }
        [HttpPost]

        public ActionResult CsvUpload(HttpPostedFileBase file)
        {
            var Result = "";

         

            if (file.ContentLength > 0)
            {

                var reader = new StreamReader(file.InputStream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var profile = new Profile();

                    profile.Forename = values[0] ?? "";
                    profile.Surname = values[1] ?? "";
                    profile.EmailAddress = values[2];
                    profile.ThirdPartyOptOut = false;
                    profile.Professional = false;

                    var login = new PasswordLogin();

                    login.Username = values[2];
                    login.Password = values[3];

                    var user = new User();

                    user.EmailAddress = values[2];

                    try
                    {
                 
                        RegistrationService.RegisterActivatedAccount(profile, user, login);
                        WorkUnit.Commit();

                        AuthenticationService.AuthenticateNonActiveuserWithPassword(login.Username, login.Password, true);

                        SecurityContext.SetCurrentUser(null, false);

                        Session["SuperUser"] = null;

                        Result = Result + values[2] + " Added" + System.Environment.NewLine;
                    }
                    catch (AuthenticatedException)
                    {
                        Result = Result + values[2] + " Can not be logged in" + System.Environment.NewLine;
                    }
                    catch (DuplicateUsernameException)
                    {
                        Result = Result + values[2] + " Already added" + System.Environment.NewLine;
                    }



                }

                Random rnd = new Random();
                int card = rnd.Next(100000);
                var fileName = Path.GetFileName(file.FileName + card);
                var path = Path.Combine(Server.MapPath("~/App_Data/CSVuploads"), fileName);
                file.SaveAs(path);



            }

            return Json(Result);
        }


    }
}