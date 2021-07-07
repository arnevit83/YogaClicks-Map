using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoMapper;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.MailingLists;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Models.Shared;
using Microsoft.Ajax.Utilities;
using Image = Clicks.Yoga.Domain.Entities.Image;
using Clicks.Yoga.Portal.Enums.Profiles;
using Clicks.Yoga.Geography;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.SignUp;
using Clicks.Yoga.Portal.Models.Teachers;
using Clicks.Yoga.Portal.Models.Venues;
using CodeBits;
using Profile = Clicks.Yoga.Domain.Entities.Profile;

namespace Clicks.Yoga.Portal.Controllers
{
    public class SignUpController : YogaController
    {
        public SignUpController(
            IWorkUnit workUnit,
            IAuthenticationService authenticationService,
            IActivityService activityService,
            ISecurityContext securityContext,
            IAccountService accountService,
            IAddressService addressService,
            IEntityService entityService,
            IFanService fanService,
            IFriendService friendService,
            IImageService imageService,
            INotificationService notificationService,
            IReviewService reviewService,
            IRequestService requestService,
            IProfileService profileService,
            IWebsiteService websiteService,
            ITeacherService teacherService,
            IVenueService venueService,
            ITeacherTrainingService teacherTrainingService,
            INewsService newsService,
            IGroupService groupService,
            IRegistrationService registrationService,
            IEmailService emailService,
            IInvitationService invitationService,
            IMailingListProvider mailingListProvider,
            IStyleService styleService,
            IMedicService medicService,
            IAccreditationBodyService accreditationBodyService,
            IQualificationService qualificationService,
            ISchoolService schoolService
            )
            : base(workUnit, securityContext)
        {
            ActivityService = activityService;
            AccountService = accountService;
            AuthenticationService = authenticationService;
            AddressService = addressService;
            EntityService = entityService;
            FanService = fanService;
            FriendService = friendService;
            ImageService = imageService;
            NotificationService = notificationService;
            ReviewService = reviewService;
            RequestService = requestService;
            ProfileService = profileService;
            WebsiteService = websiteService;
            TeacherService = teacherService;
            VenueService = venueService;
            TeacherTrainingService = teacherTrainingService;
            NewsService = newsService;
            GroupService = groupService;
            MailingListProvider = mailingListProvider;
            RegistrationService = registrationService;
            InvitationService = invitationService;
            EmailService = emailService;
            StyleService = styleService;
            MedicService = medicService;
            AccreditationBodyService = accreditationBodyService;
            QualificationService = qualificationService;
            SchoolService = schoolService;
        }

        private IActivityService ActivityService { get; set; }

        private IAccountService AccountService { get; set; }

        private IAuthenticationService AuthenticationService { get; set; }

        private IAddressService AddressService { get; set; }

        private IEntityService EntityService { get; set; }

        private IFanService FanService { get; set; }

        private IFriendService FriendService { get; set; }

        private IImageService ImageService { get; set; }

        private INotificationService NotificationService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IRequestService RequestService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IVenueService VenueService { get; set; }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        private INewsService NewsService { get; set; }

        private IGroupService GroupService { get; set; }

        private IMailingListProvider MailingListProvider { get; set; }

        private IRegistrationService RegistrationService { get; set; }

        private IEmailService EmailService { get; set; }

        private IInvitationService InvitationService { get; set; }

        private IStyleService StyleService { get; set; }

        private IMedicService MedicService { get; set; }

        private IAccreditationBodyService AccreditationBodyService { get; set; }

        private IQualificationService QualificationService { get; set; }

        private ISchoolService SchoolService { get; set; }

        public ActionResult Start()
        {
            var model = new SignUpStartViewModel();
            Profile profile = this.SecurityContext.CurrentProfile;
            if (profile != null)
            {
                model.HasProflile = true;

                var teacher = TeacherService.GetTeacherForUser(profile.Id);
                if (teacher != null)
                {
                    model.HasTeacher = true;
                }
            }

            return View("Index", model);
        }
        public ActionResult Welcome()
        {
            var model = new SignUpStartViewModel();
        
            model.HasProflile = false;
            model.HasTeacher = false;
            return View("Index", model);
        }
        [YogaAuthorize]
        public ActionResult StartEdit()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult EmailTest()
        {
            return View("Email");
        }


        [HttpPost]
        public ActionResult Invite(InviteEmailViewModel model)
        {
            try
            {
                var toemailarray = model.ToEmail.Split(',');

                foreach (var email in toemailarray)
                {
                    EmailService.SendEmail(email, "Invite from YogaClicks", model.Message);
                }

                return Content("true");
            }
            catch (Exception)
            {
                return Content("false");
            }
        }

        [HttpGet]
        public ActionResult Profile(int step)
        {
            switch (step)
            {
                case 1:
                    // Getting started
                    return View("profile1");
                case 2:
                    // Lets Get Personal
                    var model = new ProfileStep2ViewModel()
                    {
                        TypeOfSignUpValue = ProfileStep2ViewModel.TypeOfSignUp.Profile
                    };

                    Profile profile = this.SecurityContext.CurrentProfile;
                    if (profile != null)
                    {
                        model.PopulateViewModel(profile);
                    }
                    else
                    {
                        model.BirthDate = new SignUpDateEditorModel(null)
                        {
                            MinmumYear = DateTime.Now.Year - 150,
                            MaximumYear = DateTime.Now.Year
                        }
                        ;
                    }

                    return View("profile2", model);
                case 3:
                    // Small Quiz
                    return View("profile3");
                case 4:
                    // Congrats Page
                    return View("profile4");
                case 5:
                    // True Self
                    var questions = ProfileService.GetProfileQuestions();
                    var answers = ProfileService.GetProfileAnswersForCurrentUser();
                    return View("profile5", new ProfileStep5ViewModel(questions, answers));
                default:
                    return RedirectToAction("Start");
            }
        }


        [HttpGet]
        public ActionResult ProfileDone()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile != null)
            {
                return RedirectToAction("Display", "Profiles", new { id = profile.Id });
            }

            return RedirectToAction("Profile", new { step = 1 });
        }

        [HttpGet]
        public ActionResult ProfileTrueSelf()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile != null)
            {
                return RedirectToAction("Update", "Profiles", new { id = profile.Id });
            }

            return RedirectToAction("Profile", new { step = 1 });
        }

        [HttpGet]
        public ActionResult ProfileNewsFeed()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile != null)
            {
                return RedirectToAction("News", "Profiles", new { id = profile.Id });
            }

            return RedirectToAction("Profile", new { step = 1 });
        }

        [HttpGet]
        public ActionResult Teacher(int step)
        {
            Profile profile = this.SecurityContext.CurrentProfile;
            Teacher teacher;
            switch (step)
            {
                case 1:
                    // Getting started
                    return View("teacher1");
                case 2:
                    // Lets Get Personal
                    var model = new ProfileStep2ViewModel()
                    {
                        TypeOfSignUpValue = ProfileStep2ViewModel.TypeOfSignUp.Teacher
                    };

                    if (profile != null)
                    {
                        //Already a profile so skip to the next page
                        return RedirectToAction("Teacher", new { step = 3 });
                    }
                    else
                    {
                        Session["newTeacher"] = "true";
                        model.BirthDate = new SignUpDateEditorModel(null)
                        {
                            MinmumYear = DateTime.Now.Year - 150,
                            MaximumYear = DateTime.Now.Year
                        }
                        ;
                    }

                    return View("profile2", model);
                case 3:
                    //Teacher Creation Page

                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("Teacher", new { step = 2 });
                    }
                    teacher = TeacherService.GetTeacherForUser(profile.Id);
                    if (teacher == null)
                    {
                        var modelstep3 = new TeacherStep3ViewModel();
                        return View("Teacher3", modelstep3);
                    }
                    else
                    {
                        var modelstep3 = new TeacherStep3ViewModel(teacher, AddressService.GetCountries(), profile);
                        Session["editTeacherProfile"] = "true";
                        return View("Teacher3", modelstep3);
                    }

                case 4:
                    //Teacher training
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("Teacher", new { step = 2 });
                    }
                    teacher = TeacherService.GetTeacherForUser(profile.Id);
                    if (teacher == null)
                    {
                        return RedirectToAction("Teacher", new { step = 3 });
                    }
                    if (Session["editTeacherProfile"] != null)
                    {
                        var modelstep4 = new TeacherStep4ViewModel(teacher, AccreditationBodyService, true);
                        return View("Teacher4", modelstep4);
                    }
                    else
                    {
                        var modelstep4 = new TeacherStep4ViewModel(teacher, AccreditationBodyService);
                        return View("Teacher4", modelstep4);
                    }
                case 5:
                    // Teacher Styles and Services
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("Teacher", new { step = 2 });
                    }
                    teacher = TeacherService.GetTeacherForUser(profile.Id);
                    if (teacher == null)
                    {
                        return RedirectToAction("Teacher", new { step = 3 });
                    }
                    else
                    {
                        if (Session["editTeacherProfile"] != null)
                        {
                            var modelstep5 = new TeacherStep5ViewModel(teacher);
                            modelstep5.PopulateMetadata(StyleService, TeacherService, MedicService, true);
                            return View("Teacher5", modelstep5);
                        }
                        else
                        {
                            var modelstep5 = new TeacherStep5ViewModel(teacher);
                            modelstep5.PopulateMetadata(StyleService, TeacherService, MedicService);
                            return View("Teacher5", modelstep5);
                        }
                    }
                case 6:
                    // Additional
                    Session["newTeacher"] = "false";
                    return View("Teacher6");
                case 7:
                    // Congrats Page
                    return View("Teacher7");
                default:
                    return RedirectToAction("Start");
            }
        }

        [HttpGet]
        public ActionResult TeacherSchedule()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher == null)
            {
                return RedirectToAction("Teacher", new { step = 1 });
            }

            return RedirectToAction("Activities", "Activities");
        }


        [HttpGet]
        public ActionResult TeacherGroups()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher == null)
            {
                return RedirectToAction("Teacher", new { step = 1 });
            }

            return RedirectToAction("Groups", "Teachers", new { id = teacher.Id });
        }

        [HttpGet]
        public ActionResult TeacherPhotos()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher == null)
            {
                return RedirectToAction("Teacher", new { step = 1 });
            }

            return RedirectToAction("Galleries", "Teachers", new { id = teacher.Id });
        }

        [HttpGet]
        public ActionResult TeacherDone()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher == null)
            {
                return RedirectToAction("Teacher", new { step = 1 });
            }

            return RedirectToAction("Display", "Teachers", new { id = teacher.Id });
        }

        [HttpGet]
        public ActionResult Venue(int step, int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;
            Venue venue;

            switch (step)
            {
                case 1:
                    // Getting started
                    return View("venue1");
                case 2:
                    // Lets Get Personal
                    var model = new ProfileStep2ViewModel()
                    {
                        TypeOfSignUpValue = ProfileStep2ViewModel.TypeOfSignUp.Venue
                    };
                    if (profile != null)
                    {
                        //Already a profile so skip to the next page
                        return RedirectToAction("Venue", new { step = 3 });
                    }
                    else
                    {
                        Session["newVenue"] = "true";
                        model.BirthDate = new SignUpDateEditorModel(null)
                        {
                            MinmumYear = DateTime.Now.Year - 150,
                            MaximumYear = DateTime.Now.Year
                        }
                        ;
                    }

                    return View("profile2", model);
                case 3:
                    //Venue Creation Page
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("Venue", new { step = 2 });
                    }

                    if (id == 0)
                    {
                        var modelstep3 = new VenueStep3ViewModel(AddressService.GetCountries());
                        return View("Venue3", modelstep3);
                    }
                    else
                    {
                        venue = VenueService.GetVenueForEditing(id);
                        var modelstep3 = new VenueStep3ViewModel(venue, AddressService.GetCountries(), profile);
                        return View("Venue3", modelstep3);
                    }
                case 4:
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("Venue", new { step = 2 });
                    }

                    venue = VenueService.GetVenueForEditing(id);
                    if (venue == null)
                    {
                        return RedirectToAction("Venue", new { step = 3 });
                    }
                    var modelstep4 = new VenueStep4ViewModel(venue, VenueService, StyleService, MedicService);
                    return View("Venue4", modelstep4);
                case 5:
                    // Additional
                    Session["newVenue"] = "false";
                    var modelstep5 = new SignUpIdModel() { Id = id };
                    var teacher = TeacherService.GetTeacherForUser(profile.Id);
                    if (teacher != null)
                    {
                        modelstep5.HasTeacher = true;
                    }
                    return View("Venue5", modelstep5);
                case 6:
                    // Congrats Page
                    var modelstep6 = new SignUpIdModel() { Id = id };
                    return View("Venue6", modelstep6);
                default:
                    return RedirectToAction("Start");
            }
        }


        [HttpGet]
        public ActionResult VenueSchedule(int step, int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            return RedirectToAction("Activities", "Activities");
        }

        [HttpGet]
        public ActionResult VenuePhotos(int step, int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            return RedirectToAction("Galleries", "Venues", new { id });
        }

        [HttpGet]
        public ActionResult VenueDone(int step, int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }
            
            return RedirectToAction("Display", "Venues", new { id });
        }

        [HttpGet]
        public ActionResult TeacherTrainingOrganisation(int step, int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;
            TeacherTrainingOrganisation tto;
            switch (step)
            {
                case 1:
                    // Getting started
                    return View("TTO1");
                case 2:
                    // Lets Get Personal
                    var model = new ProfileStep2ViewModel()
                    {
                        TypeOfSignUpValue = ProfileStep2ViewModel.TypeOfSignUp.Tto
                    };
                    if (profile != null)
                    {
                        //Already a profile so skip to the next page
                        return RedirectToAction("TeacherTrainingOrganisation", new { step = 3 });
                    }
                    else
                    {
                        Session["newTto"] = "true";
                        model.BirthDate = new SignUpDateEditorModel(null)
                        {
                            MinmumYear = DateTime.Now.Year - 150,
                            MaximumYear = DateTime.Now.Year
                        }
                        ;
                    }

                    return View("profile2", model);
                case 3:
                    //Tto Creation Page
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("TeacherTrainingOrganisation", new { step = 2 });
                    }
                    try
                    {
                        if (id == 0)
                        {
                            var modelstep3 = new TtoStep3ViewModel(AddressService.GetCountries());
                            return View("Tto3", modelstep3);
                        }
                        else
                        {
                            tto = TeacherTrainingService.GetOrganisationForEditing(id);
                            Session["editTtoProfile"] = id;
                            var modelstep3 = new TtoStep3ViewModel(tto, AddressService.GetCountries(), profile);
                            return View("Tto3", modelstep3);
                        }
                    }
                    catch (Exception)
                    {
                        var modelstep3 = new TtoStep3ViewModel(AddressService.GetCountries());
                        return View("Tto3", modelstep3);
                    }

                case 4:
                    //Tto training
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("TeacherTrainingOrganisation", new { step = 2 });
                    }
                    tto = TeacherTrainingService.GetOrganisationForEditing(id);

                    if (Session["editTtoProfile"] != null && Session["editTtoProfile"].ToString() == id.ToString())
                    {
                        var modelstep4 = new TtoStep4ViewModel(tto, AccreditationBodyService, true);
                        return View("Tto4", modelstep4);
                    }
                    else
                    {
                        var modelstep4 = new TtoStep4ViewModel(tto, AccreditationBodyService);
                        return View("Tto4", modelstep4);
                    }

                case 5:
                    // Tto Styles and Services
                    if (profile == null)
                    {
                        //No user profile so go back a page
                        return RedirectToAction("Teacher", new { step = 2 });
                    }

                    tto = TeacherTrainingService.GetOrganisationForEditing(id);

                    if (Session["editTtoProfile"] != null && Session["editTtoProfile"].ToString() == id.ToString())
                    {
                        var modelstep5 = new TtoStep5ViewModel(tto, StyleService, MedicService, true);
                        return View("Tto5", modelstep5);
                    }
                    else
                    {
                        var modelstep5 = new TtoStep5ViewModel(tto, StyleService, MedicService);
                        return View("Tto5", modelstep5);
                    }
                case 6:
                    // Additional
                    Session["newTto"] = "false";
                    return View("Tto6");
                case 7:
                    // Congrats Page
                    return View("Tto7");
                default:
                    return RedirectToAction("Start");
            }

        }

        [HttpGet]
        public ActionResult TeacherTrainingOrganisationSchedule()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var tto = TeacherTrainingService.GetOrganisationForUser(profile.Id);

            if (tto == null)
            {
                return RedirectToAction("TeacherTrainingOrganisation", new { step = 1 });
            }

            return RedirectToAction("Schedule", "TeacherTrainingOrganisations", new { id = tto.Id });
        }

        [HttpGet]
        public ActionResult TeacherTrainingOrganisationGroup()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var tto = TeacherTrainingService.GetOrganisationForUser(profile.Id);

            if (tto == null)
            {
                return RedirectToAction("TeacherTrainingOrganisation", new { step = 1 });
            }

            return RedirectToAction("Groups", "TeacherTrainingOrganisations", new { id = tto.Id });
        }

        [HttpGet]
        public ActionResult TeacherTrainingOrganisationDone()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile == null)
            {
                return RedirectToAction("Profile", new { step = 1 });
            }

            var tto = TeacherTrainingService.GetOrganisationForUser(profile.Id);

            if (tto == null)
            {
                return RedirectToAction("TeacherTrainingOrganisation", new { step = 1 });
            }

            return RedirectToAction("Display", "TeacherTrainingOrganisations", new { id = tto.Id });
        }


        [HttpPost]
        public ActionResult Profile(ProfileStep2ViewModel userProfile)
        {

            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("Address.Latitude")) ModelState.Remove("Address.Latitude");
                if (ModelState.ContainsKey("Address.Longitude")) ModelState.Remove("Address.Longitude");

                if (!userProfile.Image.NewImage.Exists)
                {
                    if (ModelState.ContainsKey("Image.ImageCrop.XCoord")) ModelState.Remove("Image.ImageCrop.XCoord");
                    if (ModelState.ContainsKey("Image.ImageCrop.YCoord")) ModelState.Remove("Image.ImageCrop.YCoord");
                    if (ModelState.ContainsKey("Image.ImageCrop.Width")) ModelState.Remove("Image.ImageCrop.Width");
                    if (ModelState.ContainsKey("Image.ImageCrop.Height")) ModelState.Remove("Image.ImageCrop.Height");
                }
                if (!userProfile.ProfileBanner.NewImage.Exists)
                {
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.XCoord")) ModelState.Remove("ProfileBanner.ImageCrop.XCoord");
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.YCoord")) ModelState.Remove("ProfileBanner.ImageCrop.YCoord");
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.Width")) ModelState.Remove("ProfileBanner.ImageCrop.Width");
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.Height")) ModelState.Remove("ProfileBanner.ImageCrop.Height");
                }
            }

            if (!ModelState.IsValid) return View("profile2", userProfile);

            Profile profile = this.SecurityContext.CurrentProfile;

            if (profile != null)
            {
                //Update Profile
                userProfile.PopulateEntity(profile, WebsiteService, ProfileService, ImageService);
                profile.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("Profile", "SignUp", new { step = 5 });
            }
            else
            {
                //Create new user
                profile = new Profile
                {
                    Forename = userProfile.Forename ?? "",
                    Surname = userProfile.Surname ?? "",
                    EmailAddress = userProfile.EmailAddress
                };

                userProfile.PopulateEntity(profile, WebsiteService, ProfileService, ImageService);
                profile.ModificationTime = DateTime.Now;
                WorkUnit.Commit();

                var login = new PasswordLogin { Username = userProfile.EmailAddress, Password = userProfile.Password };
                var user = new User { EmailAddress = userProfile.EmailAddress };


                try
                {
                    RegistrationService.RegisterActivatedAccount(profile, user, login);
                    WorkUnit.Commit();

                    AuthenticationService.AuthenticateNonActiveuserWithPassword(login.Username, login.Password, true);

                    switch (userProfile.TypeOfSignUpValue)
                    {
                        case ProfileStep2ViewModel.TypeOfSignUp.Profile:
                            return RedirectToAction("Profile", new { step = 3 });
                        case ProfileStep2ViewModel.TypeOfSignUp.Teacher:
                            return RedirectToAction("Teacher", new { step = 3 });
                        case ProfileStep2ViewModel.TypeOfSignUp.Venue:
                            return RedirectToAction("Venue", new { step = 3 });
                        case ProfileStep2ViewModel.TypeOfSignUp.Tto:
                            return RedirectToAction("TeacherTrainingOrganisation", new { step = 3 });
                        default:
                            return RedirectToAction("Start");
                    }

                }
                catch (DuplicateUsernameException)
                {
                    ModelState.AddModelError("EmailAddress", Resources.Errors.Registration.UsernameExists);
                    return View("profile2", userProfile);
                }
            }
        }

        [HttpPost]
        public ActionResult Profile5(ProfileStep5ViewModel model)
        {

            Profile profile = this.SecurityContext.CurrentProfile;
            var questions = ProfileService.GetProfileQuestions();
            var answers = model.ProfileQuestions.CreateAnswers(profile, questions);
            ProfileService.UpdateProfileQuestions(profile, answers);
            WorkUnit.Commit();

            if (profile != null)
            {
                return RedirectToAction("Display", "Profiles", new { id = profile.Id });
            }

            return RedirectToAction("Profile", new { step = 1 });
        }

        [HttpPost]
        public ActionResult TeacherStep3(TeacherStep3ViewModel teacherProfile)
        {
            Profile profile = this.SecurityContext.CurrentProfile;
            Teacher teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher != null)
            {
                //Update Teacher Step 3
                teacherProfile.PopulateEntity(teacher, WebsiteService, ImageService, TeacherService);
                teacher.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
            }
            else
            {
                var newTeacher = TeacherService.CreateTeacher(profile.Owner);
                newTeacher.EmailAddress = profile.EmailAddress;
                teacherProfile.PopulateEntity(newTeacher, WebsiteService, ImageService, TeacherService);
                newTeacher.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
            }

            return RedirectToAction("Teacher", "SignUp", new { step = 4 });
        }

        [HttpPost]
        public ActionResult TeacherStep4(TeacherStep4ViewModel teacherProfile)
        {
            Profile profile = this.SecurityContext.CurrentProfile;
            Teacher teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher != null)
            {
                //Update Teacher Step 4
                teacherProfile.PopulateEntity(teacher, AccreditationBodyService, QualificationService, SchoolService);
                teacher.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
            }
            else
            {
                return RedirectToAction("Teacher", "SignUp", new { step = 2 });
            }

            return RedirectToAction("Teacher", "SignUp", new { step = 5 });
        }

        [HttpPost]
        public ActionResult TeacherStep5(TeacherStep5ViewModel teacherProfile)
        {
            Profile profile = this.SecurityContext.CurrentProfile;
            Teacher teacher = TeacherService.GetTeacherForUser(profile.Id);

            if (teacher != null)
            {
                //Update Profile
                teacherProfile.PopulateEntity(teacher, StyleService, TeacherService, MedicService);
                teacher.ModificationTime = DateTime.Now;
                WorkUnit.Commit();

                if (Session["editTeacherProfile"] != null)
                {
                    return RedirectToAction("Display", "Teachers", new { id = teacher.Id });
                }

                return RedirectToAction("Teacher", "SignUp", new { step = 6 });
            }
            else
            {
                return RedirectToAction("Teacher", "SignUp", new { step = 2 });
            }
        }


        [HttpPost]
        public ActionResult VenueStep3(VenueStep3ViewModel venueProfile)
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            if (venueProfile.Id > 0)
            {
                //Update Venue Step 3
                Venue venue = VenueService.GetVenueForEditing(venueProfile.Id);
                venueProfile.PopulateEntity(venue, WebsiteService, ImageService, TeacherService, AddressService);
                venue.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("Venue", "SignUp", new { step = 4, id = venueProfile.Id });
            }

            //Create new venue
            var newVenue = VenueService.CreateVenue(profile.Owner);
            newVenue.EmailAddress = profile.EmailAddress;
            venueProfile.PopulateEntity(newVenue, WebsiteService, ImageService, TeacherService, AddressService);
            newVenue.ModificationTime = DateTime.Now;
            WorkUnit.Commit();
            return RedirectToAction("Venue", "SignUp", new { step = 4, id = newVenue.Id });

        }

        [HttpPost]
        public ActionResult VenueStep4(VenueStep4ViewModel venueProfile)
        {
            Venue venue = VenueService.GetVenueForEditing(venueProfile.Id);

            if (venue != null)
            {
                //Update Venue Step 4
                venueProfile.PopulateEntity(venue, StyleService, VenueService, MedicService);
                venue.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
            }
            else
            {
                return RedirectToAction("Venue", "SignUp", new { step = 2 });
            }

            return RedirectToAction("Venue", "SignUp", new { step = 5 , id = venue.Id});
        }


        [HttpPost]
        public ActionResult TtoStep3(TtoStep3ViewModel ttoProfile)
        {

            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("Address.Latitude")) ModelState.Remove("Address.Latitude");
                if (ModelState.ContainsKey("Address.Longitude")) ModelState.Remove("Address.Longitude");

                if (!ttoProfile.Image.NewImage.Exists)
                {
                    if (ModelState.ContainsKey("Image.ImageCrop.XCoord")) ModelState.Remove("Image.ImageCrop.XCoord");
                    if (ModelState.ContainsKey("Image.ImageCrop.YCoord")) ModelState.Remove("Image.ImageCrop.YCoord");
                    if (ModelState.ContainsKey("Image.ImageCrop.Width")) ModelState.Remove("Image.ImageCrop.Width");
                    if (ModelState.ContainsKey("Image.ImageCrop.Height")) ModelState.Remove("Image.ImageCrop.Height");
                }
                if (!ttoProfile.ProfileBanner.NewImage.Exists)
                {
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.XCoord")) ModelState.Remove("ProfileBanner.ImageCrop.XCoord");
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.YCoord")) ModelState.Remove("ProfileBanner.ImageCrop.YCoord");
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.Width")) ModelState.Remove("ProfileBanner.ImageCrop.Width");
                    if (ModelState.ContainsKey("ProfileBanner.ImageCrop.Height")) ModelState.Remove("ProfileBanner.ImageCrop.Height");
                }
            }

            if (!ModelState.IsValid) return View("Tto3", ttoProfile);

            Profile profile = this.SecurityContext.CurrentProfile;
            User user = AccountService.GetUser(profile.Id);

            if (ttoProfile.Id > 0)
            {
                //Update Tto Step 3
                TeacherTrainingOrganisation tto = TeacherTrainingService.GetOrganisationForEditing(ttoProfile.Id);
                ttoProfile.PopulateEntity(tto, user, WebsiteService, ImageService, AddressService);
                tto.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("TeacherTrainingOrganisation", "SignUp", new { step = 4, id = tto.Id });
            }

            var organisation = new TeacherTrainingOrganisation();
            ttoProfile.PopulateEntity(organisation, user, WebsiteService, ImageService, AddressService);
            TeacherTrainingService.AddOrganisation(organisation);
            organisation.ModificationTime = DateTime.Now;
            WorkUnit.Commit();
            return RedirectToAction("TeacherTrainingOrganisation", "SignUp", new { step = 4, id = organisation.Id });
        }

        [HttpPost]
        public ActionResult TtoStep4(TtoStep4ViewModel ttoProfile)
        {
            TeacherTrainingOrganisation tto = TeacherTrainingService.GetOrganisationForEditing(ttoProfile.Id);

            if (tto != null)
            {
                //Update Tto Step 4
                ttoProfile.PopulateEntity(tto, AccreditationBodyService, QualificationService, SchoolService);
                tto.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
            }
            else
            {
                return RedirectToAction("TeacherTrainingOrganisation", "SignUp", new { step = 2 });
            }

            return RedirectToAction("TeacherTrainingOrganisation", "SignUp", new { step = 5, id = tto.Id });
        }

        [HttpPost]
        public ActionResult TtoStep5(TtoStep5ViewModel ttoProfile)
        {
            TeacherTrainingOrganisation tto = TeacherTrainingService.GetOrganisationForEditing(ttoProfile.Id);

            if (tto != null)
            {
                //Update Profile
                ttoProfile.PopulateEntity(tto, StyleService, TeacherService, MedicService);
                tto.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("TeacherTrainingOrganisation", "SignUp", new { step = 6, id = tto.Id });
            }
            else
            {
                return RedirectToAction("TeacherTrainingOrganisation", "SignUp", new { step = 2 });
            }
        }


        [HttpGet]
        public ActionResult VenueTest()
        {
            var venuemodel = new VenueTest();
            venuemodel.Venue.PopulateMetadata(AddressService.GetCountries());

            return View("Venuetest", venuemodel);
        }

        [HttpPost]
        public ActionResult VenueTest(VenueTest model)
        {
            var venuemodel = new VenueTest();
            venuemodel.Venue.PopulateMetadata(AddressService.GetCountries());

            return View("Venuetest", venuemodel);
        }







        //Notification in use yet, this is how teacher placements are created.
        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreatePlacement(int id, TeacherCreatePlacementModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(id);

            if (!ModelState.IsValid) return View(model);

            var venue = model.Venue.Id.HasValue ? VenueService.GetVenue(model.Venue.Id.Value) : null;

            if (venue != null && venue.Owner != null)
            {
                TeacherService.AddPlacement(teacher, venue);
                WorkUnit.Commit();
                return View("CloseModal", new CloseModalModel(null, "AddTeacherVenue"));
            }

            if (venue == null || venue.Owner == null)
            {
                return View("CreatePlacementOwnership", new TeacherCreatePlacementOwnershipModel(model.Venue.Id, model.Venue.Name));
            }

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreatePlacementOwnership(int id, TeacherCreatePlacementOwnershipModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(id);

            Venue venue = null;

            if (model.VenueId.HasValue)
            {
                venue = VenueService.GetVenue(model.VenueId.Value);
            }
            else
            {
                return View("CreatePlacementAddress", new TeacherCreatePlacementAddressModel(model.VenueName, model.Owned, model.EmailAddress).PopulateMetadata(AddressService));
            }

            if (model.Owned)
            {
                if (venue.Owner != null) throw new PermissionDeniedException();

                venue.Stubbed = false;
                venue.Owner = teacher.Owner;
            }

            TeacherService.AddPlacement(teacher, venue);

            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(null, "AddTeacherVenue"));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreatePlacementAddress(int id, TeacherCreatePlacementAddressModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(id);

            if (!ModelState.IsValid) return View(model.PopulateMetadata(AddressService));

            var venue = new Venue
            {
                Owner = model.Owned ? teacher.Owner : null,
                Name = model.VenueName,
                Address = new Address(),
                Stubbed = !model.Owned
            };

            model.Address.PopulateEntity(venue.Address, AddressService);

            VenueService.AddVenue(venue);
            TeacherService.AddPlacement(teacher, venue);

            var invite = EntityService.InviteEntity(venue, SecurityContext.CurrentUser, model.EmailAddress);

            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(null, "AddTeacherVenue"));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult RemovePlacement(int teacherId, int venueId)
        {
            var venue = VenueService.GetVenue(venueId);
            var teacher = TeacherService.GetTeacher(teacherId);

            if (teacher == null || venue == null)
                return Json(false);

            if (!SecurityContext.CurrentUser.CanUpdate(teacher) && !SecurityContext.CurrentUser.CanUpdate(venue))
                return Json(false);

            var placement = teacher.Placements.FirstOrDefault(p => p.Venue.Id == venue.Id);

            if (placement == null)
                return Json(false);

            TeacherService.RemovePlacement(placement);
            WorkUnit.Commit();

            return Json(true);
        }
    }
}