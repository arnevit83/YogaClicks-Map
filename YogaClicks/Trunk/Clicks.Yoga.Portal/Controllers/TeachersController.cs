using System;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Geography;
using Clicks.Yoga.Portal.Models.Shared;
using Clicks.Yoga.Portal.Models.Teachers;
using Clicks.Yoga.Portal.Models.Venues;
using Clicks.Yoga.MailingLists;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.App_Start;

namespace Clicks.Yoga.Portal.Controllers
{
    public class TeachersController : YogaController
    {
        public TeachersController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IAddressService addressService,
            IEntityService entityService,
            IImageService imageService,
            IReviewService reviewService,
            IStyleService styleService,
            IMedicService medicService,
            ITeacherService teacherService,
            IWebsiteService websiteService,
            IAccreditationBodyService accreditationBodies,
            IVenueService venueService,
            IProfileService profileService,
            IQualificationService qualificationService,
            IMailingListProvider mailingListProvider,
            ITeacherTrainingService teacherTrainingService)

            : base(workUnit, securityContext)
        {
            AddressService = addressService;
            EntityService = entityService;
            ImageService = imageService;
            ReviewService = reviewService;
            StyleService = styleService;
            TeacherService = teacherService;
            WebsiteService = websiteService;
            AccreditationBodyService = accreditationBodies;
            VenueService = venueService;
            ProfileService = profileService;
            MailingListProvider = mailingListProvider;
            MedicService = medicService;
            QualificationService = qualificationService;
            TeacherTrainingService = teacherTrainingService;
        }

        private IProfileService ProfileService { get; set; }

        private IQualificationService QualificationService { get; set; }

        private IAddressService AddressService { get; set; }

        private IEntityService EntityService { get; set; }

        private IImageService ImageService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IStyleService StyleService { get; set; }

        private IMedicService MedicService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        private IAccreditationBodyService AccreditationBodyService { get; set; }

        private IVenueService VenueService { get; set; }

        private IMailingListProvider MailingListProvider { get; set; }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        [RequireRequestValue("id")]
        public ActionResult Display(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            var profile = ProfileService.GetProfile(teacher);
            var visible = profile != null && ProfileService.ProfileVisibleToCurrentUser(profile.Id);
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;

            return View(new TeacherDisplayModel(teacher, profile, venue, tto, visible ));
        }
        
        [RequireRequestValue("teacherslug")]
        public ActionResult Display(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherString(teacherslug);

            if (teacher == null)
            {
                Response.Redirect("/search/teachers?Keywords=" + teacherslug);
            }
            else
            {
                ViewBag.TeacherId = teacher.Id;
                ViewBag.TeachTextSearch = teacherslug;
            }
            
            ChangeActorIfOwner(teacher);

            var profile = ProfileService.GetProfile(teacher);
            var visible = profile != null && ProfileService.ProfileVisibleToCurrentUser(profile.Id);
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;
            
            return View("Display", new TeacherDisplayModel(teacher, profile, venue, tto, visible));
        }

        [RequireRequestValue("id")]
        public ActionResult Fans(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View("Fans", new TeacherFansModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Fans(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Fans", new TeacherFansModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Reviews(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            var reviews = ReviewService.GetReviewsBySubject(teacher);
            
            return View(new TeacherReviewsModel(teacher, reviews));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Reviews(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);

            var reviews = ReviewService.GetReviewsBySubject(teacher);
            ViewBag.TeachTextSearch = teacherslug;
            
            return View("Reviews", new TeacherReviewsModel(teacher, reviews));
        }

        [RequireRequestValue("id")]
        public ActionResult Galleries(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherGalleriesModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Galleries(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);

            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Galleries", new TeacherGalleriesModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Gallery(int id, int galleryId)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherGalleryModel(teacher, galleryId));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Gallery(string teacherslug, int galleryId)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Gallery", new TeacherGalleryModel(teacher, galleryId));
        }

        [RequireRequestValue("id")]
        public ActionResult Videos(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherVideosModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Videos(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Videos", new TeacherVideosModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Placements(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherPlacementsModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Placements(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Placements", new TeacherPlacementsModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Classes(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherClassesModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Classes(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Classes", new TeacherClassesModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Schedule(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherClassesModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Schedule(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Schedule", new TeacherClassesModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Activities(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherActivitiesModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Activities(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Activities", new TeacherActivitiesModel(teacher));
        }

        [RequireRequestValue("id")]
        public ActionResult Groups(int id)
        {
            var teacher = TeacherService.GetTeacherForDisplay(id);

            foreach (var result in EnsureUrlSlug(teacher)) return result;

            ChangeActorIfOwner(teacher);

            return View(new TeacherGroupsModel(teacher));
        }

        [RequireRequestValue("teacherslug")]
        public ActionResult Groups(string teacherslug)
        {
            var teacher = TeacherService.GetTeacherForDisplayString(teacherslug);
            
            ChangeActorIfOwner(teacher);
            ViewBag.TeachTextSearch = teacherslug;

            return View("Groups", new TeacherGroupsModel(teacher));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStubbed(string name, string location, double latitude, double longitude, string email)
        {
            var teacher = TeacherService.CreateStubbed(name, new Location { Name = location, Coordinates = new GeographicPoint(latitude, longitude) });
            WorkUnit.Commit();
            var invite = EntityService.InviteEntity(teacher, SecurityContext.CurrentUser, email);
            WorkUnit.Commit();

            return Json(teacher.Id);
        }
        
        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var teacher = TeacherService.GetTeacherForEditing(id);

            return View(new TeacherUpdateModel(teacher).PopulateMetadata(teacher, StyleService, MedicService, TeacherService, AccreditationBodyService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, TeacherUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(id);

            if (!ModelState.IsValid) return View(model.PopulateMetadata(teacher, StyleService, MedicService, TeacherService, AccreditationBodyService));

            model.PopulateEntity(teacher, ImageService, StyleService, MedicService, TeacherService, AccreditationBodyService, WebsiteService, QualificationService);

            teacher.ModificationTime = DateTime.Now;

            if (model.NewsletterOptOut)
            {
                MailingListProvider.Unsubscribe(MailingList.Teachers, teacher.EmailAddress);
            }
            else
            {
                MailingListProvider.Subsribe(MailingList.Teachers, teacher.EmailAddress, teacher.Name, "");
            }

            WorkUnit.Commit();

            return RedirectToAction("Display");
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateBanner(TeacherBannerUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, ImageService);
            teacher.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateImage(TeacherImageUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, ImageService);
            teacher.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateDetails(TeacherDetailsUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, WebsiteService);
            teacher.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateQualifications(TeacherQualificationsUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, QualificationService);
            teacher.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateNewsletterOpt(TeacherNewsletterOptUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher);
            teacher.ModificationTime = DateTime.Now;

            if (model.NewsletterOptOut)
            {
                MailingListProvider.Unsubscribe(MailingList.Teachers, teacher.EmailAddress);
            }
            else
            {
                MailingListProvider.Subsribe(MailingList.Teachers, teacher.EmailAddress, teacher.Name, "");
            }

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateLocation(TeacherLocationUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher);
            teacher.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateServices(TeacherServicesUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, TeacherService);
            teacher.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateStyles(TeacherStylesUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, StyleService);
            teacher.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateConditions(TeacherConditionsUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, MedicService);
            teacher.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdatePhilosophy(TeacherPhilosophyUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher);
            teacher.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateAccreditations(TeacherAccreditationsUpdateModel model)
        {
            var teacher = TeacherService.GetTeacherForEditing(model.TeacherId);

            model.PopulateEntity(teacher, AccreditationBodyService);
            teacher.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.TeacherId);
        }


        [YogaAuthorize]
        public ActionResult CreatePlacement(int id)
        {
            var teacher = TeacherService.GetTeacherForEditing(id);

            return View(new TeacherCreatePlacementModel());
        }

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