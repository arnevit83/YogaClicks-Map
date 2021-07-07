using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Emails;
using Clicks.Yoga.Media.Scanners;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using Clicks.Yoga.Portal.Models.SignUp;
using Clicks.Yoga.Portal.Models.Stories;
using Clicks.Yoga.Portal.Models.YogaMap;
using Clicks.Yoga.Portal.Resources.Errors;

namespace Clicks.Yoga.Portal.Controllers
{
    public class YogaMapController : YogaController
    {
  
        public YogaMapController(
            IWorkUnit workUnit,
            IAuthenticationService authenticationService,
            IActivityService activityService,
            ISecurityContext securityContext,
            IAccountService accountService,
            IAddressService addressService,
            IEntityService entityService,
            IFanService fanService,
               IVideoService videoService,
            IRegistrationService registrationService,
            IImageService imageService,
            IRequestService requestService,
            IProfileService profileService,
               IInvitationService invitationService,
            ITeacherService teacherService,
               IVenueService venueService,
            IStyleService styleService,
             IEmailService emailService,
            IYmStoryService storyService,
            IMedicService medicService
            )
            : base(workUnit, securityContext)
        {
            RegistrationService = registrationService;
            ActivityService = activityService;
            AccountService = accountService;
            AuthenticationService = authenticationService;
            AddressService = addressService;
            EntityService = entityService;
            FanService = fanService;
            ImageService = imageService;
            RequestService = requestService;
            EmailService = emailService;
            ProfileService = profileService;
            TeacherService = teacherService;
            StyleService = styleService;
            StoryService = storyService;
            MedicService = medicService;
            VideoService = videoService;
            VenueService = venueService;
            
                 InvitationService = invitationService;
        }

        private IActivityService ActivityService { get; set; }
        private IVideoService VideoService { get; set; }
        private IVenueService VenueService { get; set; }
        
        private IMedicService MedicService { get; set; }
        private IInvitationService InvitationService { get; set; }

        private IAccountService AccountService { get; set; }

        private IEmailService EmailService { get; set; }

        private IAuthenticationService AuthenticationService { get; set; }

        private IAddressService AddressService { get; set; }

        private IEntityService EntityService { get; set; }

        private IFanService FanService { get; set; }

        private IImageService ImageService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IRequestService RequestService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IStyleService StyleService { get; set; }

        private IYmStoryService StoryService { get; set; }
        private IRegistrationService RegistrationService { get; set; }

        [HttpPost]
        public ActionResult AuthenticateWithFacebookYogaMap(string accessToken)
        {
            AddStoryPopUpModelUpdate model;
            try
            {

                 model = new AddStoryPopUpModelUpdate(new YmStory(), MedicService, AddressService, TeacherService, VenueService,StoryService);
            var temp = AuthenticationService.AuthenticateWithFacebookForYogaMap(accessToken);
          
                
                
                var scanner = new ImageWebMediaScanner(ImageService);
                var image =
                    scanner.GetAcceptableImage("http://graph.facebook.com/" + temp[0] +
                                               "/picture?type=large&redirect=true&width=500&height=500");
                var tempimage = new StoryImageEditorModel(image);

                WorkUnit.Commit();
           // image.PopulateEntity(() => FBImage, ImageService);

      
            

                // KLUDGE: If a user has been created as part of the authentication process,
                // its id will be 0 until the work unit has been committed.
                SecurityContext.SetCurrentUser(SecurityContext.CurrentUser, false);
                
                    model.FbLocation = temp[1];
             
             
        
                Profile profile = SecurityContext.CurrentProfile;
                if (profile != null)
                {
                    model.Displaymodel.HasProflile = true;
                    model.Displaymodel.Email = profile.EmailAddress;
                    model.Displaymodel.Forename = profile.Forename;
                    model.Image = tempimage;
                    if (image != null)
                    {
                        model.Fbimage = image.Id;
                    }
                
                  
                    model.Displaymodel.Surname = profile.Surname;
                    model.Displaymodel.Name = profile.Forename + " " + profile.Surname;
                }
                else
                {
                    model.Displaymodel.HasProflile = false;
                }
            }
            catch (DuplicateUsernameException)
            {
                return Json("");
            }
            SecurityContext.CurrentProfile.NumberofLoginIn = SecurityContext.CurrentProfile.NumberofLoginIn + 1;
            SecurityContext.CurrentProfile.LastLoggedIn = DateTime.Now;

            SecurityContext.CurrentProfile.HasStory = StoryService.HasStory(SecurityContext.CurrentProfile.Id);
            SecurityContext.CurrentProfile.YogaPro = ProfileService.GetProfessionalEntities().Count > 0;
            WorkUnit.Commit();

            return  View("AddStoryPopUp", model) ;
        }


        

        public ActionResult Index()
        {
            var model = new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext);
            return View("Display", model);
        }
        public ActionResult YogaMapApp()
        {
            var model = new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext);
            return View("YogaMapApp", model);
        }


        




        [HttpPost]
        public ActionResult Authenticatemap(MapLogin model)
        {
            if (!ModelState.IsValid)
            {
                model.ValidationMap = "Incorrect email / password combination";
                return PartialView("MapLogin", model);
            }

            model.Destination = string.IsNullOrEmpty(model.Destination) ? "/" : model.Destination;

            if (SecurityContext.Authenticated)
            {
                return PartialView("MapLogin", model);
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
                    model.ValidationMap = "Your account has not been activated. Check your inbox for the activation email and follow the link provided";
                }
                else
                {
                    model.ValidationMap = "Incorrect email / password combination";
                }
            }

            return PartialView("MapLogin", model);
        }


        [HttpPost]
        public ActionResult GetBigPopup(int id)
        {

         
            var objecttest = StoryService.GetStory(id);
    
            if (objecttest == null)
            {
                var modelError = new YogaMap(MedicService.GetConditions(),AccountService,SecurityContext);
                return Json("");
            }

            var model = new YogaMap(objecttest, MedicService.GetConditions(), AccountService, SecurityContext);
              var teacher = TeacherService.GetTeacherForUser(model.ViewModel.OwnerId);
            if (teacher != null)
            {
                model.ViewModel.Teacher = new Models.Entities.TeacherModel(teacher);
            }
           
            Object[] bigStory = new Object[7];



            bigStory[0] = model.ViewModel.Title;
            bigStory[1] = model.ViewModel.Story;
            bigStory[2] = model.ViewModel.Conditions;
            bigStory[3] = "";
            bigStory[4] = "";
            bigStory[5] = "";
            bigStory[6] = "";

            if (objecttest.LifeChallenges.Count > 0)
            {
                bigStory[6] = "Life Challenges: ";
                foreach (var challenge in objecttest.LifeChallenges)
                {
                    bigStory[6] += challenge.Name + " | ";



                }


            }
            if (bigStory[6].ToString() != "")
            {
                bigStory[6] = bigStory[6].ToString().Substring(0, bigStory[6].ToString().Length - 2);
            }

            if (objecttest.Teachers.Count > 0)
            {
                bigStory[4] = "Inspiring teachers: ";
                foreach (var teacherfav in objecttest.Teachers)
                {
                    bigStory[4] += "<a href='" + 
                                       @Url.Action("Display", "Teachers",
                                           new { teacherfav.Id }) + "'>" + teacherfav.Name + "</a> | ";



                }


            }
            if (bigStory[4].ToString() != "")
            {
                bigStory[4] = bigStory[4].ToString().Substring(0, bigStory[4].ToString().Length - 2);
            }
            if (objecttest.Venues.Count > 0)
            {
                bigStory[5] = "Inspiring venues: ";
                foreach (var venuefav in objecttest.Venues)
                {
                    bigStory[5] += "<a href='" +
                                       @Url.Action("Display", "Venues",
                                           new { venuefav.Id }) + "'>" + venuefav.Name + "</a> | ";
                }


            }
            if (bigStory[5].ToString() != "")
            {
                bigStory[5] = bigStory[5].ToString().Substring(0, bigStory[5].ToString().Length - 2);
            }
            if (model.ViewModel.Type == "Shop")
            {
                bigStory[3] += "<a target='_blank' href="+ model.ViewModel.ShopUrl + "><img class='IconG' data-toggle='tooltip' title='View the shop' src='/images/svgicons/shopicon.svg' data-svgpng='/images/SvgIcons/shopicon.png'/></a>";
            }
            else
            {

                if (SecurityContext.Authenticated)
                {



                    if (model.ViewModel.OwnerId == SecurityContext.CurrentProfile.Id)
                    {
                        bigStory[3] += "<a href=" + @Url.Action("Welcome", "signup") +
                                       "><img class='IconG' data-toggle='tooltip' title='Create a full profile' src='/images/svgicons/Icon_ProfileGreen.svg' data-svgpng='/images/SvgIcons/Icon_ProfileGreen.png'/></a>";
                    }
                    else
                    {
                        if (model.ViewModel.Teacher != null && model.ViewModel.Type == "Teacher")
                        {
                            bigStory[3] += "<a href=" +
                                           @Url.Action("Display", "Teachers",
                                               new { Id = model.ViewModel.Teacher.Id }) +
                                           "><img class='IconG' data-toggle='tooltip' title='View their teacher profile' src='/images/svgicons/Icon_TeacherPurple.svg' data-svgpng='/images/SvgIcons/Icon_TeacherPurple.png'/></a>";

                        }
                        else
                        {
                            bigStory[3] += "<a href=" +
                                           @Url.Action("Display", "Profiles",
                                               new { Id = model.ViewModel.OwnerId }) +
                                           "><img class='IconG' data-toggle='tooltip' title='View their profile' src='/images/svgicons/Icon_ProfileBlue.svg' data-svgpng='/images/SvgIcons/Icon_ProfileBlue.png'/></a>";
                        }

                    }
                }
                else
                {



                    if (model.ViewModel.Teacher != null && model.ViewModel.Type == "Teacher")
                    {
                        bigStory[3] += "<a href=" +
                                       @Url.Action("Display", "Teachers",
                                           new {Id = model.ViewModel.Teacher.Id}) +
                                       "><img class='IconG' data-toggle='tooltip' title='View their teacher profile' src='/images/svgicons/Icon_TeacherPurple.svg' data-svgpng='/images/SvgIcons/Icon_TeacherPurple.png'/></a>";

                    }
                    else
                    {
                        bigStory[3] += "<a href=" +
                                       @Url.Action("Display", "Profiles",
                                           new {Id = model.ViewModel.OwnerId}) +
                                       "><img class='IconG' data-toggle='tooltip' title='View their profile' src='/images/svgicons/Icon_ProfileBlue.svg' data-svgpng='/images/SvgIcons/Icon_ProfileBlue.png'/></a>";
                    }
                
            }



            }



            return Json(bigStory);


        }




        public ActionResult Display(int id)
        {
            var objecttest = StoryService.GetStory(id);
    
            if (objecttest == null)
            {
                var modelError = new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext);
                return View("Display", modelError);
            }



            var model = new YogaMap(objecttest, MedicService.GetConditions(), AccountService, SecurityContext);
            var teacher = TeacherService.GetTeacherForUser(model.ViewModel.OwnerId);
            if (teacher != null)
            {
                model.ViewModel.Teacher = new Models.Entities.TeacherModel(teacher);
            }


         

            return View("Display", model);
        }


        [HttpPost]
        public ActionResult GetMapStoriesConditions()
        {
            return Json(new MapStoriesModel(StoryService.GetMapStoriesConditions(), SecurityContext));
        }
   

        [HttpPost]
        public ActionResult GetMapStoriesByArea()
        {
            return Json(new MapStoriesModel(StoryService.GetStories(), SecurityContext));
        }

        
        [HttpPost]
        public ActionResult GetMapStoriesByBucket(int bucket)
        {
            return Json(new MapStoriesModel(StoryService.GetMapStoriesBucket(bucket), SecurityContext));
        }






        



        
        [HttpPost]
        public ActionResult GetMapStories()
        {
            return Json(new MapStoriesModel(StoryService.GetStories(), SecurityContext));
        }


        [HttpPost]
        public ActionResult Search(string TSreturn, int? Condition)
        {
            var model = StoryService.SearchStories(TSreturn, Condition);
            return Json(new MapStoriesModel(model, SecurityContext));
        }



        [HttpGet]
        public ActionResult AddStoryPopUp()
        {
            var model = new AddStoryPopUpModelUpdate(new YmStory(), MedicService, AddressService, TeacherService, VenueService, StoryService);

            Profile profile = SecurityContext.CurrentProfile;
            if (profile != null)
            {
                model.Displaymodel.HasProflile = true;
                model.Displaymodel.Email = profile.EmailAddress;
                model.Displaymodel.Forename = profile.Forename;
                model.Displaymodel.Surname = profile.Surname;
                model.Displaymodel.Name = profile.Forename  + " " + profile.Surname;
            
            }
            else
            {
                model.Displaymodel.HasProflile = false;
            }
            //if (AccountService.CurrentUserHasFederatedLogin("Facebook"))
            //{

            //    var client = new FacebookClient(accessToken);


            //}






            return View(model);
        }

        [HttpPost]
        public ActionResult AddStoryPopUpInvalid(AddStoryPopUpModelUpdate model)
        {
            ModelState.AddModelError("Displaymodel.Email", Account.email_address_Taken_story);
            var Conditions = new SignUpConditionChooserModel();
            Conditions.PopulateMetadata(MedicService.GetConditions());
            model.Displaymodel.Conditions = Conditions;
            var lifeChallengeChooser = new LifeChallengeChooserModel();
            lifeChallengeChooser.PopulateMetadata(StoryService.GetLifeChallenges());
            model.Displaymodel.LifeChallenges = lifeChallengeChooser;

            return View("AddStoryPopUp", model);


        }

        [HttpPost]
        public ActionResult AddStoryPopUp(AddStoryPopUpModelUpdate model)
        {
          


            if (model == null)
            {
                return View("Display", new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext));
            }



            if (model.Displaymodel.HasProflile)
            {
                ModelState["Displaymodel.Forename"].Errors.Clear();
                ModelState["Displaymodel.Surname"].Errors.Clear();
                ModelState["Displaymodel.Email"].Errors.Clear();
                ModelState["Displaymodel.Password"].Errors.Clear();
               

            }

            if (model.Displaymodel.IsupdateId > 0)
            {
                ModelState["Displaymodel.address.Latitude"].Errors.Clear();

            }


            var profile = SecurityContext.CurrentProfile;


             
            if (!ModelState.IsValid)
            {

                //model.PopulateMetadata(MedicService);
 
                //if (profile != null)
                //{
                //    model.Displaymodel.Name = profile.Forename + " " + profile.Surname;
                //}
                //return PartialView("AddStoryPopUp", model);

                //var returnModel = new YogaMap(MedicService.GetConditions()) {AddStoryupdateModel = model.Displaymodel};


                //return View("Display", returnModel);
                var returnModel = new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext) { AddStoryupdateModel = model };
                returnModel.AddStoryupdateModel.Image = new StoryImageEditorModel();

                return View("Display", returnModel);



            }



            var newStory = new YmStory();

            if (model.Displaymodel.IsupdateId > 0)
            {
             newStory = StoryService.GetStory(model.Displaymodel.IsupdateId);
            }

         
       


            if (profile != null)
            {
                //CreateStory



                model.PopulateEntity(newStory, profile, MedicService, ImageService, AddressService, TeacherService, VenueService,StoryService);


                if (SecurityContext.Authenticated)
                {
                    if (AccountService.CurrentUserHasFederatedLogin("Facebook"))
                    {
                        if (newStory.Image == null & model.Fbimage != 0)
                        {
                            newStory.Image = ImageService.GetImage(model.Fbimage);
                        }


                    }
                    
                }
            




                if (model.Displaymodel.VideoUrl != null)
                {
                    try
                    {
                        var video = VideoService.CreateVideo(model.Displaymodel.VideoUrl);
                        WorkUnit.Commit();
                        newStory.VideoUpload = video;
                    }
                    catch (Exception)
                    {
                      
                    }
               
                }


                if (model.Displaymodel.IsupdateId == 0)
                {

                    StoryService.AddStory(newStory);
                }
          


          
                WorkUnit.Commit();

                if (model.Displaymodel.IsupdateId == 0)
                {
                    foreach (var Venue in newStory.Venues)
                    {
                        try
                        {
                            if (Venue.EmailAddress != null)
                            {
                                EmailService.SendStoryEmail(Venue.EmailAddress, Venue.Name, newStory.Name,
                                    newStory.Id);
                            }
                        }
                        catch { }
                    }
                    foreach (var Teacher in newStory.Teachers)
                    {
                        try
                        {
                            if (Teacher.EmailAddress != null)
                            {
                                EmailService.SendStoryEmail(Teacher.EmailAddress, Teacher.Name, newStory.Name, newStory.Id);
                            }
                        }
                        catch { }
                    }

                }



          
            



                var modelView = new YogaMap(newStory, MedicService.GetConditions(), AccountService, SecurityContext);
                var teacher = TeacherService.GetTeacherForUser(modelView.ViewModel.OwnerId);
                if (teacher != null)
                {
                    modelView.ViewModel.Teacher = new Models.Entities.TeacherModel(teacher);
                }
                return View("Display", modelView);

            }
            else
            {
                //Create new user
                profile = new Profile
                {
                    Forename = model.Displaymodel.Forename ?? "",
                    Surname = model.Displaymodel.Surname ?? "",
                    EmailAddress = model.Displaymodel.Email,
                    ModificationTime = DateTime.Now
                };



                var login = new PasswordLogin { Username = profile.EmailAddress, Password = model.Displaymodel.Password };
                var user = new User { EmailAddress = profile.EmailAddress };

                try
                {
                    var invitationCookie = Request.Cookies["Invitation"];
                    var invitationIdentifier = invitationCookie != null ? invitationCookie.Value : null;

                    RegistrationService.RegisterActivatedAccount(profile, user, login);
                    InvitationService.IntroduceInvitee(profile, invitationIdentifier);
           



                    WorkUnit.Commit();
                    AuthenticationService.AuthenticateNonActiveuserWithPassword(login.Username, login.Password, true);
                    EmailService.SendWelcomeEmail(user);
                }
             
                catch (DuplicateUsernameException)
                {
                   
                    //var Conditions = new SignUpConditionChooserModel();
                    //Conditions.PopulateMetadata(MedicService.GetConditions());
                    //model.Displaymodel.Conditions = Conditions;

                    //return View(model);
                    var returnModel = new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext) { AddStoryupdateModel = model };

                 
                    returnModel.AddStoryupdateModel.Image = new StoryImageEditorModel();

                    return View("Display", returnModel);


                }

                //CreateStory
                model.PopulateEntity(newStory, profile, MedicService, ImageService, AddressService,TeacherService,VenueService,StoryService);
                




                if (model.Displaymodel.VideoUrl != null)
                {
                  

                    try
                    {
                        var video = VideoService.CreateVideo(model.Displaymodel.VideoUrl);
                        WorkUnit.Commit();
                        newStory.VideoUpload = video;
                    }
                    catch (Exception)
                    {

                    }
                }

                StoryService.AddStory(newStory);
                WorkUnit.Commit();
                if (model.Displaymodel.IsupdateId == 0)
                {
                    foreach (var Venue in newStory.Venues)
                    {
                        try
                        {
                            if (Venue.EmailAddress != null)
                            {
                                EmailService.SendStoryEmail(Venue.EmailAddress, Venue.Name, newStory.Name,
                                    newStory.Id);
                            }
                        }
                        catch { }
                    }
                    foreach (var Teacher in newStory.Teachers)
                    {
                        try
                        {
                            if (Teacher.EmailAddress != null)
                            {
                                EmailService.SendStoryEmail(Teacher.EmailAddress, Teacher.Name, newStory.Name, newStory.Id);
                            }
                        }
                        catch { }
                    }

                }


                var modelView = new YogaMap(newStory, MedicService.GetConditions(), AccountService, SecurityContext);
                var teacher = TeacherService.GetTeacherForUser(modelView.ViewModel.OwnerId);
                if (teacher != null)
                {
                    modelView.ViewModel.Teacher = new Models.Entities.TeacherModel(teacher);
                }




                return View("Display", modelView);



            }
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(true)]
        //public ActionResult NominateEmail(YogaMap model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            EmailService.SendStoryEmail(model.EmailAddress, model.ToName, model.FromName);
        //            model.FilererrorEmail = "<div class='alert alert-success' role='alert'>Thank you for sharing The Yoga Map</div>";
        //            model.ToName = "";
        //            model.EmailAddress = "";
        //            model.FromName = "";



        //            var conditions =
        //                MedicService.GetConditions().Select(condition => new NamedSummaryModel(condition)).ToList();


        //            model.Conditions = conditions;
        //            return View("Display", model);
        //        }
        //        else
        //        {
        //            var conditions = MedicService.GetConditions().Select(condition => new NamedSummaryModel(condition)).ToList();
        //            model.Conditions = conditions;
        //            return View("Display", model);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        model.FilererrorEmail = "<div class='alert alert-danger' role='alert'>Please try again</div>"; ;
        //        var conditions = MedicService.GetConditions().Select(condition => new NamedSummaryModel(condition)).ToList();


        //        model.Conditions = conditions;
        //        return View("Display" ,model);
        //    }
        //}
        [YogaAuthorize]
        public ActionResult RemoveStory(int id)
        {
     
            var profile = SecurityContext.CurrentProfile;
            var story = StoryService.GetStory(id);
            if (profile.Owner.Id != story.Owner.Id)
            {
                var objecttest = StoryService.GetStory(id);
                var viewmodel = new YogaMap(objecttest, MedicService.GetConditions(), AccountService, SecurityContext);
                return View("Display", viewmodel);

            }

            StoryService.RemoveStory(story);
            WorkUnit.Commit();
            var model = new YogaMap(MedicService.GetConditions(), AccountService, SecurityContext);
            return View("Display", model);



        }
        [YogaAuthorize]
        [HttpPost]
        public ActionResult MoveStory(int id, float lng, float lat)
        {

            var profile = SecurityContext.CurrentProfile;
            var story = StoryService.GetStory(id);
            if (profile.Owner.Id != story.Owner.Id)
            {
                var objecttest = StoryService.GetStory(id);
                var viewmodel = new YogaMap(objecttest, MedicService.GetConditions(), AccountService, SecurityContext);
                return View("Display", viewmodel);

            }

            story.Lng = lng;
            story.Lat = lat;


            WorkUnit.Commit();
            return Json("success");



        }
                [YogaAuthorize]
        [HttpPost]
        public ActionResult AddStoryPopEdit(int id)
        {
            var profile = SecurityContext.CurrentProfile;
            var story = StoryService.GetStory(id);

            if (profile.Owner.Id != story.Owner.Id)
            {
                var objecttest = StoryService.GetStory(id);
                var viewmodel = new YogaMap(objecttest, MedicService.GetConditions(), AccountService, SecurityContext);
                return View("Display", viewmodel);

            }


            var Conditions = new SignUpConditionChooserModel(story.Conditions);

            Conditions.PopulateMetadata(MedicService.GetConditions());



            var Challenge = new LifeChallengeChooserModel(story.LifeChallenges);

            Challenge.PopulateMetadata(StoryService.GetLifeChallenges());



            var image = new StoryImageEditorModel(story.Image);

                    image.PopulateEntity(() => story.Image, ImageService);
               



            var model = new AddStoryPopUpModelUpdate
            {
                Image = image,
                
                TSreturn = story.ProfileType,
                YearTaughtreturn = story.PoweredFrom.Year,
                Displaymodel =
                {
                    Name = profile.Forename + " " + profile.Surname,
                    Title = story.Title,
                    Story = story.Story,
                    Address = new YogaMapAddressEditorModel(),
                    HasProflile = true,
                    Lanpop = story.Lng.ToString(),
                    Latpop = story.Lat.ToString(),
                    VideoUrl = story.Video,
                    Conditions = Conditions,
                    LifeChallenges = Challenge,
                    IsupdateId = id,
                    Teachers =  new TeacherChooserModel(story.Teachers),
                    Venues = new VenueChooserModel(story.Venues)
                }
            };
       
            return View("AddStoryPopUp", model);


        }

        public ActionResult AssociatedPartialprofile(int ownerId, string entityTypeName)
        {

            IList<YmStory> stories =  new List<YmStory>();
            if (ownerId != 0)
            {


                stories = StoryService.GetStories(ownerId, entityTypeName);

            ((List<YmStory>)stories).AddRange(StoryService.GetStories(ownerId, "Shop"));

            if (TeacherService.GetTeacherForUser(ownerId) == null)
            {
                if (entityTypeName == "Student")
                {
                    ((List<YmStory>)stories).AddRange(StoryService.GetStories(ownerId, "Teacher"));
                }
        
            }

            }
            return View("AssociatedPartialprofile", new AssociatedPartialProfileModel(stories, MedicService.GetConditions()));
        }



        public ActionResult AssociatedPartialFav(int ownerId, string entityTypeName, string proType)
        {



            IList<YmStory> stories = StoryService.GetStoriesFav(ownerId, entityTypeName, proType);
            var header = "";
            if (entityTypeName == "Teacher")
            {
                header = "Our teacher stories";
            }
            if (entityTypeName == "Student")
                {
                    header = "Our student stories";
                }

            if (entityTypeName == "TeacherStudent")
            {
                header = "My student stories";
            }

            var model = new AssociatedPartialProfileModel(stories, MedicService.GetConditions()) { Header = header };
            return View("AssociatedPartialprofile", model);
        }
    
    }

}