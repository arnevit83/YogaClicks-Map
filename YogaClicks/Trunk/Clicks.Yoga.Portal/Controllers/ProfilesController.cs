using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.MailingLists;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Models.Shared;
using Microsoft.Ajax.Utilities;
using Image = Clicks.Yoga.Domain.Entities.Image;
using Clicks.Yoga.Portal.Enums.Profiles;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Portal.Controllers
{
    public class ProfilesController : YogaController
    {
        public ProfilesController(
            IWorkUnit workUnit,
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
            IMailingListProvider mailingListProvider)
            : base(workUnit, securityContext)
        {
            AccountService = accountService;
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
        }

        private IAccountService AccountService { get; set; }

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

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [YogaAuthorize]
        public ActionResult Navigation(int id)
        {
            var profile = ProfileService.GetProfile(id);

            if (!SecurityContext.CanUpdate(profile))
            {
                throw new PermissionDeniedException();
            }

            var entities = ProfileService.GetProfessionalEntities();
            var groups = ProfileService.GetGroups(profile.Owner.Id);

            return PartialView("Navigation", new ProfileNavigationModel(profile, entities, groups));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ClaimButton(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IClaimable>(entityId, entityTypeName);

            if (entity.Owner == null)
            {
                var entityType = EntityService.GetEntityType(entityTypeName);

                if (!entityType.IsTeacher || !TeacherService.CurrentUserHasTeacher())
                {
                    return View(new ProfileClaimButtonModel(entityId, entityType));
                }
            }

            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult UnclaimButton(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<ISecurable>(entityId, entityTypeName);

            if (SecurityContext.IsOwner(entity))
            {
                var entityType = EntityService.GetEntityType(entityTypeName);
                return View(new ProfileUnclaimButtonModel(entityId, entityType));
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Claim(int entityId, string entityTypeName)
        {
            ProfileService.Claim(entityId, entityTypeName);
            WorkUnit.Commit();
            return View();
        }

        [HttpPost]
        public ActionResult Unclaim(int entityId, string entityTypeName)
        {
            ProfileService.Unclaim(entityId, entityTypeName);
            WorkUnit.Commit();
            return Json(new
            {
                Success = true,
                Location = Url.Action("Display", new { id = SecurityContext.CurrentProfile.Id })
            });
        }

        public ActionResult Display(int id)
        {
            var profile = ProfileService.GetProfile(id);

            if (profile != null && !profile.Active)
                throw new UnknownEntityException();

            var visible = ProfileService.ProfileVisibleToCurrentUser(id);
            var images = profile != null ? ProfileService.GetPromotedGalleryImages(profile.Owner.Id) : new List<Image>();
            var teacher = profile != null ? TeacherService.GetTeacherForUser(profile.Owner.Id) : null;
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;


            ChangeActorIfOwner(profile);

            Session["WizardStatus"] = null;

            return View(new ProfileDisplayModel(profile, teacher, venue, tto, visible, images));
        }

        [YogaAuthorize]
        public ActionResult News(int id)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            if (profile.IsFirstVisit)
            {
                profile.IsFirstVisit = false;
                WorkUnit.Commit();
                return RedirectToAction("profile-professional-guide", "static");
            }

            return View("News", new ProfileNewsModel(profile));
        }

        [YogaAuthorize]
        public ActionResult Notifications(int id)
        {
            var profile = ProfileService.GetProfileForEditing(id);
            var deliveries = NotificationService.GetRecentNotifications(profile.Owner);
            var requests = RequestService.GetPendingRequests(profile.Owner);

            var model = new ProfileNotificationsModel(deliveries, requests);

            NotificationService.Dismiss(deliveries);
            WorkUnit.Commit();

            return View(model);
        }

        public ActionResult Friends(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);
            var model = new ProfileFriendsModel(profile);

            ChangeActorIfOwner(profile);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        public ActionResult FriendsPartial(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);
            var friendships = FriendService.GetFriendships(profile.Id);

            return View("FriendsPartial", new ProfileFriendsPartialModel(profile, friendships));
        }

        [YogaAuthorize]
        public ActionResult FriendRequests(int id)
        {
            var profile = ProfileService.GetProfileForEditing(id);
            var requests = RequestService.GetPendingFriendRequests(profile.Owner);
            var model = new ProfileFriendRequestsModel(requests);

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Block(int id, int[] friendIds)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            if (friendIds != null)
            {
                foreach (var friendId in friendIds)
                {
                    FriendService.Block(profile.Id, friendId);
                }
            }

            WorkUnit.Commit();

            return FriendsPartial(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Unblock(int id, int[] friendIds)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            if (friendIds != null)
            {
                foreach (var friendId in friendIds)
                {
                    FriendService.Unblock(profile.Id, friendId);
                }
            }

            WorkUnit.Commit();

            return FriendsPartial(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Unfriend(int id, int[] friendIds)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            if (friendIds != null)
            {
                foreach (var friendId in friendIds)
                {
                    FriendService.Unfriend(profile.Id, friendId);
                }
            }

            WorkUnit.Commit();

            return new EmptyResult();
        }

        public ActionResult Fanned(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);
            var types = FanService.GetFanableTypes();
            var handles = FanService.GetFanned(profile.Owner);

            ChangeActorIfOwner(profile);

            return View(new ProfileFannedModel(profile, types, handles));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Unfan(int id, int[] entityIds, string entityTypeName)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            if (entityIds != null)
            {
                foreach (var entityId in entityIds)
                {
                    FanService.Unfan(SecurityContext.CurrentUser, entityId, entityTypeName);
                }
            }

            WorkUnit.Commit();

            var type = EntityService.GetEntityType(entityTypeName);
            var handles = FanService.GetFanned(profile.Owner, entityTypeName);
            var editable = SecurityContext.IsOwner(profile);

            if (type == null) return new EmptyResult();

            return PartialView("FannedGroupPartial", new ProfileFannedGroupPartialModel(type, handles, editable));
        }

        public ActionResult Activities(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileActivitiesModel(profile));
        }

        [YogaAuthorize]
        public ActionResult Classes(int id)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileClassesModel(profile));
        }

        public ActionResult Reviews(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileReviewsModel(profile));
        }

        public ActionResult Galleries(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileGalleriesModel(profile));
        }

        public ActionResult Gallery(int id, int galleryId)
        {
            var profile = ProfileService.GetProfileForDisplay(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileGalleryModel(profile, galleryId));
        }

        public ActionResult Videos(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileVideosModel(profile));
        }

        public ActionResult Groups(int id)
        {
            var profile = ProfileService.GetProfileForDisplay(id);

            ChangeActorIfOwner(profile);

            return View(new ProfileGroupsModel(profile,
                GroupService.GetUnmanagedGroups(profile.Owner.Id),
                GroupService.GetManagedGroups(profile.Owner.Id)));
        }

        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var profile = ProfileService.GetProfileForEditing(id);
            var questions = ProfileService.GetProfileQuestions();
            var answers = ProfileService.GetProfileAnswersForCurrentUser();

            Session["WizardStatus"] = null;

            return View(new ProfileUpdateModel(profile, questions, answers));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, ProfileUpdateModel model)
        {
            var profile = ProfileService.GetProfileForEditing(id);
            var questions = ProfileService.GetProfileQuestions();
            var answers = ProfileService.GetProfileAnswersForCurrentUser();

            if (!ModelState.IsValid)
            {
                return View(new ProfileUpdateModel(profile, questions, answers));
            }

            model.PopulateEntity(profile, SecurityContext, ProfileService, WebsiteService, ImageService);

            profile.ModificationTime = DateTime.Now;

            if (profile.NewsletterOptOut)
            {
                MailingListProvider.Unsubscribe(MailingList.Newsletter, profile.Owner.EmailAddress);
                MailingListProvider.Unsubscribe(MailingList.Students, profile.Owner.EmailAddress);
            }
            else
            {
                MailingListProvider.Subsribe(MailingList.Newsletter, profile.Owner.EmailAddress, profile.Forename, profile.Surname);
                MailingListProvider.Subsribe(MailingList.Students, profile.Owner.EmailAddress, profile.Forename, profile.Surname);
            }

            answers = model.ProfileQuestions.CreateAnswers(profile, questions);

            ProfileService.UpdateProfileQuestions(profile, answers);

            WorkUnit.Commit();

            return RedirectToAction("Display");
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateBanner(ProfileBannerUpdateModel model)
        {
            var profile = ProfileService.GetProfileForEditing(model.ProfileId);
            model.PopulateEntity(profile, ImageService);
            profile.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.ProfileId);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateProfileImage(ProfileImageUpdateModel model)
        {
            var profile = ProfileService.GetProfileForEditing(model.ProfileId);
            model.PopulateEntity(profile, ImageService);
            profile.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.ProfileId);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdatePersonalDetails(ProfilePersonalDetailsUpdateModel model)
        {
            var profile = ProfileService.GetProfileForEditing(model.ProfileId);
            model.PopulateEntity(profile, WebsiteService, ProfileService);
            profile.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return PartialView(new ProfilePersonalDetailsUpdateModel(profile));
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateProfileNewsletterOpt(ProfilePersonalNewsletterOptUpdateModel model)
        {
            var profile = ProfileService.GetProfileForEditing(model.ProfileId);
            model.PopulateEntity(profile);
            profile.ModificationTime = DateTime.Now;

            if (profile.NewsletterOptOut)
            {
                MailingListProvider.Unsubscribe(MailingList.Newsletter, profile.Owner.EmailAddress);
                MailingListProvider.Unsubscribe(MailingList.Students, profile.Owner.EmailAddress);
            }
            else
            {
                MailingListProvider.Subsribe(MailingList.Newsletter, profile.Owner.EmailAddress, profile.Forename, profile.Surname);
                MailingListProvider.Subsribe(MailingList.Students, profile.Owner.EmailAddress, profile.Forename, profile.Surname);
            }

            WorkUnit.Commit();

            return PartialView(new ProfilePersonalNewsletterOptUpdateModel(profile));
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateProfileLocation(ProfileLocationUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", model.ProfileId);
            }

            var profile = ProfileService.GetProfileForEditing(model.ProfileId);
            model.PopulateEntity(profile);
            profile.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.ProfileId);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateProfileQuestionsAnswers(ProfileQuestionsUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", model.ProfileId);
            }

            var questions = ProfileService.GetProfileQuestions();
            var answers = ProfileService.GetProfileAnswersForCurrentUser();
            var profile = ProfileService.GetProfileForEditing(model.ProfileId);

            profile.ModificationTime = DateTime.Now;
            answers = model.ProfileQuestions.CreateAnswers(profile, questions);
            ProfileService.UpdateProfileQuestions(profile, answers);

            WorkUnit.Commit();

            return RedirectToAction("Update", model.ProfileId);
        }

        [ChildActionOnly]
        public ActionResult DeleteButton(int entityId, string entityTypeName, ISecurable securable)
        {
            if (!SecurityContext.CanDelete(securable)) return new EmptyResult();

            var entity = EntityService.GetEntity<IEntity>(entityId, entityTypeName);

            if (entity == null) return new EmptyResult();

            return PartialView(new DeleteButtonModel(entity.Id, EntityService.GetEntityType(entity.GetEntityTypeName()), ProfileService.GetProfile(securable)));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult DeleteOwned(int entityId, string entityType)
        {
            ProfileService.DeleteOwned(entityId, entityType);
            WorkUnit.Commit();

            return new EmptyResult();
        }

        [YogaAuthorize]
        public ActionResult Create()
        {
            var wizard = WizardStatus.None;

            if (Session["WizardStatus"] != null)
                wizard = (WizardStatus)Session["WizardStatus"];

            return View(new ProfileCreateModel(TeacherService.CurrentUserHasTeacher(), wizard));
        }

        [YogaAuthorize]
        public ActionResult CreateProfileManagePage(bool isNewUser)
        {
            Session["IsNewUser"] = isNewUser;

            if (isNewUser)
                return RedirectToAction("Create");

            return View("CreateManagePage");
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateProfileManagePage(ProfileCreateManagePageModel model)
        {
            if (SecurityContext.CurrentProfile.Professional)
                return RedirectToAction("Display", new { SecurityContext.CurrentProfile.Id });

            if (model.isNewUser)
            {
                switch (model.ManageBy)
                {
                    case ProfileCreateManagePageModel.ManagePage.Personal:
                        return RedirectToAction("CreateProfessionalEnd");
                    case ProfileCreateManagePageModel.ManagePage.New:
                        return RedirectToAction("MakeProfileProfessional");
                }
                return View("CreateManagePageNewUser");
            }
            switch (model.ManageBy)
            {
                case ProfileCreateManagePageModel.ManagePage.Personal:
                    return RedirectToAction("Create");
                case ProfileCreateManagePageModel.ManagePage.New:
                    return RedirectToAction("MakeNewProfessional");
            }

            return View("CreateManagePage");
        }

        [YogaAuthorize]
        public ActionResult MakeProfileProfessional()
        {
            var user = SecurityContext.CurrentProfile;

            user.Active = false;
            user.Professional = true;

            WorkUnit.Commit();

            SecurityContext.SetCurrentUser(null, false);

            return RedirectToAction("CreateAccount", "Registration");
        }

        [YogaAuthorize]
        public ActionResult MakeNewProfessional()
        {
            SecurityContext.SetCurrentUser(null, false);
            return RedirectToAction("CreateAccount", "Registration", new { professional = true });
        }

        [HttpGet]
        [YogaAuthorize]
        public ActionResult CreateProfessionalEnd()
        {
            return View();
        }

        public ActionResult CreateProfessionalCloseModal()
        {
            return View();
        }

        [HttpGet]
        [YogaAuthorize]
        public ActionResult StartWizard()
        {
            Session["WizardStatus"] = WizardStatus.First;

            if (SecurityContext.CurrentProfile.Professional)
            {
                return View("CreateWizard");
            }

            return View(new StartWizardModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult StartWizard(StartWizardModel model)
        {
            if (model.IsProfessional)
                return RedirectToAction("Create");

            return RedirectToAction("PrettifyProfile", new { id = SecurityContext.CurrentProfile.Id, profileType = ProfileTypeEnum.Personal });
        }

        [HttpGet]
        [YogaAuthorize]
        public ActionResult WizardIntro()
        {
            return View();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult WizardIntro(string s)
        {
            return RedirectToAction("UpdateStudentLocation");
        }

        [YogaAuthorize]
        public ActionResult UpdateStudentLocation(ProfileUpdateStudentLocationModel model, bool postBack = false)
        {
            var vm = model != null ? model : new ProfileUpdateStudentLocationModel();
            return View(vm);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateStudentLocation(ProfileUpdateStudentLocationModel model)
        {
            if (model.Location.HasValue == false)
            {
                ModelState.AddModelError(string.Empty, "Please add a location");
            }
            if (!model.Gender.Selected)
            {
                ModelState.AddModelError(string.Empty, "Please select a gender");
            }

            if (!ModelState.IsValid) return UpdateStudentLocation(model, true);

            var studentId = SecurityContext.CurrentProfile.Id;
            var profile = ProfileService.GetProfileForEditing(studentId);

            if (profile.Location == null)
            {
                profile.Location = new Location
                {
                    Name = model.Location.Name,
                    Coordinates = new GeographicPoint(model.Location.Latitude.Value, model.Location.Longitude.Value)
                };
            }

            profile.BirthDate = model.BirthDate.DateTime;
            if (model.Gender.Selected) profile.Gender = ProfileService.GetGender(model.Gender.Id);

            WorkUnit.Commit();

            return RedirectToAction("StartWizard");
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateWizard(int id)
        {
            return RedirectToAction("Create");
        }

        public ActionResult CreateAProfessional()
        {
            return View();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Create(int id, ProfileCreateModel model)
        {
            var profile = ProfileService.GetProfileForEditing(id);

            switch (model.Type)
            {
                case ProfileCreateType.Teacher:
                    return RedirectToAction("CreateTeacher", new { id });
                case ProfileCreateType.Venue:
                    return RedirectToAction("CreateVenue", new { id });
                case ProfileCreateType.TeacherTrainingOrganisation:
                    return RedirectToAction("Create", "TeacherTrainingOrganisations", new { UserId = profile.Owner.Id });
                case ProfileCreateType.StyleOrganisation:
                    return RedirectToAction("Create", "StyleOrganisations", new { OwnerId = profile.Owner.Id });
            }

            var status = WizardStatus.None;

            if (Session["WizardStatus"] != null)
                status = (WizardStatus)Session["WizardStatus"];

            if (status == WizardStatus.None)
            {
                model.Status = status;
                return View(model);
            }
            else
            {
                if (SecurityContext.CurrentProfile.Professional)
                {
                    return View("CloseModal", new CloseModalModel(Url.Action("Index", "Home"), "CreateGeneric"));
                }
                else
                {
                    return View("CreateManagePageNewUser");
                }
            }
        }

        [YogaAuthorize]
        public ActionResult CreateTeacher(int id)
        {
            var teacher = Session["Teacher"] as Teacher;
            var model = new ProfileCreateTeacherModel();

            if (teacher != null)
            {
                model.Name = teacher.Name;
                model.EmailAddress = teacher.EmailAddress;
                model.Telephone = teacher.Telephone;
                model.Websites = new WebsiteCollectionEditorModel(teacher.Websites);
            }
            else
            {
                model.Name = SecurityContext.CurrentProfile.Professional ? "" : SecurityContext.CurrentProfile.Name;
                Session["Teacher"] = new Teacher();
            }

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateTeacher(int id, ProfileCreateTeacherModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var teacher = (Teacher)Session["Teacher"];
            if (teacher == null) return RedirectToAction("CreateTeacher");

            model.PopulateEntity(teacher, WebsiteService);
            WorkUnit.Commit();

            return View("CreateTeacherLocation", new ProfileCreateTeacherLocationModel());
        }

        [YogaAuthorize]
        public ActionResult CreateTeacherLocation(int id)
        {
            var teacher = Session["Teacher"] as Teacher;

            if (teacher == null) return RedirectToAction("CreateTeacher");

            var model = new ProfileCreateTeacherLocationModel();

            model.Location = new LocationEditorModel(teacher.Location);

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateTeacherLocation(int id, ProfileCreateTeacherLocationModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var profile = ProfileService.GetProfileForEditing(id);

            var teacher = (Teacher)Session["Teacher"];
            if (teacher == null) return RedirectToAction("CreateTeacher");

            teacher.Owner = profile.Owner;

            model.PopulateEntity(teacher);

            TeacherService.AddTeacher(teacher);

            if (profile.Location == null)
            {
                profile.Location = teacher.Location;
            }

            NewsService.PostAction(NewsStoryType.FriendAddedEntity, profile, teacher);

            WorkUnit.Commit();
            id = teacher.Id;

            if (teacher.Owner.Id == SecurityContext.CurrentActor.OwnerId)
            {
                MailingListProvider.Subsribe(MailingList.Teachers, teacher.EmailAddress, teacher.Name, "");
                MailingListProvider.Unsubscribe(MailingList.Students, teacher.Owner.EmailAddress);
            }

            Session.Remove("Teacher");

            var wizard = WizardStatus.None;

            if (Session["WizardStatus"] != null)
                wizard = (WizardStatus)Session["WizardStatus"];

            return RedirectToAction("CongratulationsProfileCreated", new { id = teacher.Id, profileType = ProfileTypeEnum.Teacher });

        }

        [YogaAuthorize]
        public ActionResult CreateVenue(int id)
        {
            var venue = Session["Venue"] as Venue;
            var model = new ProfileCreateVenueModel();

            if (venue != null)
            {
                model.Venue.Id = venue.Id;
                model.Venue.Name = venue.Name;
            }
            else
            {
                Session["Venue"] = new Venue();
            }

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVenue(int id, ProfileCreateVenueModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var venue = (Venue)Session["Venue"];
            if (venue == null) return RedirectToAction("CreateVenue");

            model.PopulateEntity(venue);

            return View("CreateVenueContact", new ProfileCreateVenueContactModel());
        }

        [YogaAuthorize]
        public ActionResult CreateVenueContact(int id)
        {
            var venue = Session["Venue"] as Venue;
            var model = new ProfileCreateVenueContactModel();

            if (venue == null) return RedirectToAction("CreateVenue");

            model.EmailAddress = venue.EmailAddress;
            model.Telephone = venue.Telephone;
            model.Websites = new WebsiteCollectionEditorModel(venue.Websites);

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVenueContact(int id, ProfileCreateVenueContactModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var venue = (Venue)Session["Venue"];
            if (venue == null) return RedirectToAction("CreateVenue");

            model.PopulateEntity(venue, WebsiteService);
            WorkUnit.Commit();

            return View("CreateVenueLocation", new ProfileCreateVenueLocationModel().PopulateMetadata(AddressService.GetCountries()));
        }

        [YogaAuthorize]
        public ActionResult CreateVenueLocation(int id)
        {
            var venue = Session["Venue"] as Venue;
            var model = new ProfileCreateVenueLocationModel();

            if (venue == null) return RedirectToAction("CreateVenue");

            model.Address = new AddressEditorModel(venue.Address);
            model.PopulateMetadata(AddressService.GetCountries());

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVenueLocation(int id, ProfileCreateVenueLocationModel model)
        {
            if (!ModelState.IsValid) return View(model.PopulateMetadata(AddressService.GetCountries()));

            var profile = ProfileService.GetProfileForEditing(id);

            var venue = (Venue)Session["Venue"];
            if (venue == null) return RedirectToAction("CreateVenue");

            model.PopulateEntity(venue, AddressService);

            if (venue.IsTransient)
            {
                venue.Owner = profile.Owner;
                VenueService.AddVenue(venue);

                NewsService.PostAction(NewsStoryType.FriendAddedEntity, profile, venue);
            }
            else
            {
                var existing = VenueService.GetVenue(venue.Id);

                if (existing.Owner != null) throw new PermissionDeniedException();

                existing.Owner = profile.Owner;
                existing.Stubbed = false;
                existing.Name = venue.Name;
                existing.Telephone = venue.Telephone;
                existing.EmailAddress = venue.EmailAddress;

                foreach (var website in venue.Websites)
                {
                    existing.Websites.Add(website);
                }

                existing.Address = venue.Address;
            }

            WorkUnit.Commit();

            if (venue.Owner.Id == SecurityContext.CurrentActor.OwnerId)
            {
                MailingListProvider.Subsribe(MailingList.Venues, venue.EmailAddress, venue.Name, "");
                MailingListProvider.Unsubscribe(MailingList.Students, venue.Owner.EmailAddress);
            }

            Session.Remove("Venue");

            var wizard = WizardStatus.None;

            if (Session["WizardStatus"] != null)
                wizard = (WizardStatus)Session["WizardStatus"];

                return RedirectToAction("CongratulationsProfileCreated", new { id = venue.Id, profileType = ProfileTypeEnum.Venue });
        }

        [YogaAuthorize]
        public ActionResult CongratulationsProfileCreated(int id, ProfileTypeEnum profileType)
        {
            return View(new CongratulationsProfileCreatedModel { Id = id, ProfileType = profileType });
        }

        [YogaAuthorize]
        public ActionResult PrettifyProfile(int id, ProfileTypeEnum profileType)
        {
            string entityTypeLink = "";

            switch (profileType)
            {
                case ProfileTypeEnum.Personal:
                    entityTypeLink = "profiles";
                    break;
                case ProfileTypeEnum.Venue:
                    entityTypeLink = "venues";
                    break;
                case ProfileTypeEnum.Teacher:
                    entityTypeLink = "teachers";
                    break;
                case ProfileTypeEnum.TTO:
                    entityTypeLink = "teachertrainingorganisations";
                    break;
                case ProfileTypeEnum.SO:
                    entityTypeLink = "styleorganisations";
                    break;
                default:
                    break;
            }

            var profileLink = string.Format("/{0}/{1}", entityTypeLink, id);
            return View(new PrettifyProfileModel { Id = id, ProfileType = profileType, ProfileLink = profileLink });
        }

        [YogaAuthorize]
        public ActionResult ProfileExample(ProfileTypeEnum profileType)
        {
            return View(profileType);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [YogaAuthorize]
        public ActionResult FacebookConnector()
        {
            if (SecurityContext.CurrentUser.IsSuperUser || AccountService.CurrentUserHasFederatedLogin("Facebook"))
            {
                return new EmptyResult();
            }

            return View();
        }
    }
}