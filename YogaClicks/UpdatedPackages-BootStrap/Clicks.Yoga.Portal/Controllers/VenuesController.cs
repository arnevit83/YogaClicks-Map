using System;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Geography;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Shared;
using Clicks.Yoga.Portal.Models.Venues;
using Microsoft.Web.Mvc;
using Clicks.Yoga.MailingLists;

namespace Clicks.Yoga.Portal.Controllers
{
    public class VenuesController : YogaController
    {
        public VenuesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IAddressService addressService,
            IEntityService entityService,
            IImageService imageService,
            IReviewService reviewService,
            IStyleService styleService,
            IVenueService venueService,
            IWebsiteService websiteService,
            ITeacherService teacherService,
            IProfileService profileService,
            IMailingListProvider mailingListProvider)
            : base(workUnit, securityContext)
        {
            AddressService = addressService;
            EntityService = entityService;
            ImageService = imageService;
            ReviewService = reviewService;
            StyleService = styleService;
            VenueService = venueService;
            WebsiteService = websiteService;
            TeacherService = teacherService;
            ProfileService = profileService;
            MailingListProvider = mailingListProvider;
        }

        private IProfileService ProfileService { get; set; }

        private IAddressService AddressService { get; set; }

        private IEntityService EntityService { get; set; }

        private IImageService ImageService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IStyleService StyleService { get; set; }

        private IVenueService VenueService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IMailingListProvider MailingListProvider { get; set; }

        public ActionResult Display(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueDisplayModel(
                venue,
                ProfileService.GetProfile(venue),
                ReviewService.GetReviewsBySubject(venue),
                TeacherService.CurrentUserHasTeacher()));
        }

        public ActionResult Placements(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            var placements = VenueService.GetTeacherPlacements(venue);

            return View(new VenuePlacementsModel(venue, placements));
        }

        public ActionResult Fans(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View("Fans", new VenueFansModel(venue));
        }

        public ActionResult Reviews(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            var reviews = ReviewService.GetReviewsBySubject(venue);

            return View(new VenueReviewsModel(venue, reviews));
        }

        public ActionResult Galleries(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueGalleriesModel(venue));
        }

        public ActionResult Gallery(int id, int galleryId)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueGalleryModel(venue, galleryId));
        }

        public ActionResult Videos(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueVideosModel(venue));
        }

        public ActionResult Classes(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueClassesModel(venue));
        }

        public ActionResult Schedule(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueClassesModel(venue));
        }

        public ActionResult Activities(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueActivitiesModel(venue));
        }

        public ActionResult Groups(int id)
        {
            var venue = VenueService.GetVenueForDisplay(id);

            foreach (var result in EnsureUrlSlug(venue)) return result;

            ChangeActorIfOwner(venue);

            return View(new VenueGroupsModel(venue));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStubbed(string name, string location, double latitude, double longitude)
        {
            var venue = VenueService.CreateStubbed(name, new Location { Name = name, Coordinates = new GeographicPoint(latitude, longitude) });

            WorkUnit.Commit();

            return Json(venue.Id);
        }

        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var venue = VenueService.GetVenueForEditing(id);

            return View(new VenueUpdateModel(venue).PopulateMetadata(venue, AddressService, StyleService, VenueService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, VenueUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(id);

            if (!ModelState.IsValid) return View(model.PopulateMetadata(venue, AddressService, StyleService, VenueService));

            model.PopulateEntity(venue, AddressService, ImageService, StyleService, VenueService, WebsiteService);

            venue.ModificationTime = DateTime.Now;

            if (model.NewsletterOptOut)
            {
                MailingListProvider.Unsubscribe(MailingList.Venues, venue.EmailAddress);
            }
            else
            {
                MailingListProvider.Subsribe(MailingList.Venues, venue.EmailAddress, venue.Name, "");
            }

            WorkUnit.Commit();

            return RedirectToAction("Display");
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateBanner(VenueBannerUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, ImageService);
            venue.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateImage(VenueImageUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, ImageService);
            venue.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [YogaAuthorize]
        public ActionResult UpdateDetails(int id)
        {
            var venue = VenueService.GetVenueForEditing(id);

            return View(new VenueDetailsUpdateModel(venue).PopulateMetadata(venue, VenueService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateDetails(VenueDetailsUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, VenueService, WebsiteService);
            venue.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateNewsLetterOpt(VenueNewsletterOptUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue);
            venue.ModificationTime = DateTime.Now;

            if (model.NewsletterOptOut)
            {
                MailingListProvider.Unsubscribe(MailingList.Venues, venue.EmailAddress);
            }
            else
            {
                MailingListProvider.Subsribe(MailingList.Venues, venue.EmailAddress, venue.Name, "");
            }

            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateAbout(VenueAboutUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue);
            venue.ModificationTime = DateTime.Now;
           
            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateLocation(VenueLocationUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, AddressService);
            venue.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [YogaAuthorize]
        public ActionResult UpdateServices(int id)
        {
            var venue = VenueService.GetVenueForEditing(id);

            return View(new VenueServicesUpdateModel(venue).PopulateMetadata(venue, VenueService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateServices(VenueServicesUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, VenueService);
            venue.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [YogaAuthorize]
        public ActionResult UpdateAmenities(int id)
        {
            var venue = VenueService.GetVenueForEditing(id);

            return View(new VenueAmenitiesUpdateModel(venue).PopulateMetadata(venue, VenueService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateAmenities(VenueAmenitiesUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, VenueService);
            venue.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [YogaAuthorize]
        public ActionResult UpdateStyles(int id)
        {
            var venue = VenueService.GetVenueForEditing(id);

            return View(new VenueStylesUpdateModel(venue).PopulateMetadata(venue, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateStyles(VenueStylesUpdateModel model)
        {
            var venue = VenueService.GetVenueForEditing(model.VenueId);

            model.PopulateEntity(venue, StyleService);
            venue.ModificationTime = DateTime.Now;

            WorkUnit.Commit();

            return RedirectToAction("Update", model.VenueId);
        }

        [YogaAuthorize]
        public ActionResult Claim(int id)
        {
            var claimModel = new VenueClaimModel(id);
            Session.Add("claimModel", claimModel);
            var venue = VenueService.GetVenue(id);
            return View("ClaimStep1", new VenueClaimStep1Model(venue));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ClaimStep1(VenueClaimStep1Model model)
        {
            var claimModel = (VenueClaimModel)Session["claimModel"];

            if (!ModelState.IsValid) return View(model);
            claimModel.Step1Model = model;
            Session.Add("claimModel", claimModel);
            var venue = VenueService.GetVenue(claimModel.VenueId);
            return View("ClaimStep2", new VenueClaimStep2Model(venue));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ClaimStep2(VenueClaimStep2Model model)
        {
            var claimModel = (VenueClaimModel)Session["claimModel"];

            if (model.Back)
            {
                ModelState.Clear();
                return View("ClaimStep1", claimModel.Step1Model);
            }
            if (!ModelState.IsValid) return View(model);
            claimModel.Step2Model = model;
            Session.Add("claimModel", claimModel);
            var venue = VenueService.GetVenue(claimModel.VenueId);
            return View("ClaimStep3", new VenueClaimStep3Model(venue).PopulateMetadata(AddressService.GetCountries()));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ClaimStep3(VenueClaimStep3Model model)
        {
            var claimModel = (VenueClaimModel)Session["claimModel"];

            if (model.Back)
            {
                ModelState.Clear();
                return View("ClaimStep2", claimModel.Step2Model);
            }

            if (!ModelState.IsValid) return View(model);

            claimModel.Step3Model = model;
           
            var venue = VenueService.GetVenue(claimModel.VenueId);

            if (venue.Owner != null)
                throw new PermissionDeniedException();

            venue.Stubbed = false;
            venue.Owner = SecurityContext.CurrentUser;

            claimModel.PopulateEntity(venue, WebsiteService, AddressService);

            WorkUnit.Commit();
            Session.Remove("claimModel");
            return View("CloseModal", new CloseModalModel(null, "ClaimVenue"));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [YogaAuthorize]
        public ActionResult Obtain(bool showBackButton = false)
        {
            return View("ObtainStep1", new VenueObtainStep1Model(showBackButton));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ObtainStep1(VenueObtainStep1Model model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Venue.Id == null)
            {
                return View("ObtainStep2", new VenueObtainStep2Model(model.Venue.Name, model.ShowBackButton));
            }
            else
            {
                var venue = VenueService.GetVenue(model.Venue.Id.Value);

                if (venue.Stubbed)
                {
                    return View("ObtainStep2", new VenueObtainStep2Model(model.Venue.Id.Value, model.ShowBackButton));
                }
                else
                {
                    return View("ObtainStep4", new VenueObtainStep4Model(model.Venue.Id.Value));
                }
            }
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ObtainStep2(VenueObtainStep2Model model)
        {
            if (model.ObtainBack)
            {
                ModelState.Clear();
                var step1 = new VenueObtainStep1Model(model.ShowBackButton);
                step1.Venue.Name = model.Name;
                return View("ObtainStep1", step1);
            }

            if (!ModelState.IsValid) return View(model);

            if (model.Id == null)
            {
                if (model.Owned)
                {
                    var venue = VenueService.CreateVenue(SecurityContext.CurrentUser);
                    venue.Name = model.Name;
                    WorkUnit.Commit();
                    model.Id = venue.Id;
                }
                else
                {
                    var venue = VenueService.CreateStubbed(model.Name);
                    WorkUnit.Commit();
                    var invite = EntityService.InviteEntity(venue, SecurityContext.CurrentUser, model.Email);
                    WorkUnit.Commit();
                    model.Id = venue.Id;
                }
            }

            return View("ObtainStep3", new VenueObtainStep3Model(model.Id, model.Name, model.ShowBackButton).PopulateMetadata(AddressService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ObtainStep3(VenueObtainStep3Model model)
        {
            if (model.ObtainBack)
            {
                ModelState.Clear();
                var step2 = new VenueObtainStep2Model { Id = model.Id, Name = model.Name, ShowBackButton = model.ShowBackButton };
                return View("ObtainStep2", step2);
            }

            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("Address.Latitude")) ModelState.Remove("Address.Latitude");
                if (ModelState.ContainsKey("Address.Longitude")) ModelState.Remove("Address.Longitude");    
            }

            if (!ModelState.IsValid) return View(model.PopulateMetadata(AddressService));

            var venue = model.Id != null ? VenueService.GetVenue(model.Id.Value) : null;

            if (venue == null)
            {
                if (venue.Owner != null) throw new PermissionDeniedException();

                venue = VenueService.CreateVenue(SecurityContext.CurrentUser);
                venue.Name = model.Name;
            }

            venue.Address = model.Address.PopulateEntity(AddressService);

            WorkUnit.Commit();

            return View("ObtainStep4", new VenueObtainStep4Model(venue.Id));
        }

        [YogaAuthorize]
        public ActionResult CreatePlacement(int id)
        {
            return View(new VenueCreatePlacementModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreatePlacement(int id, VenueCreatePlacementModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var teacher = TeacherService.GetTeacher(model.Teacher.Id.Value);
            var venue = VenueService.GetVenue(id);

            if (teacher == null || venue == null || venue.TeacherPlacements.Any(t => t.Teacher.Id == teacher.Id))
                return View("CloseModal");

            if (!SecurityContext.CurrentUser.CanUpdate(teacher) && !SecurityContext.CurrentUser.CanUpdate(venue))
                return View("CloseModal");

            VenueService.AddPlacement(venue, teacher);
            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(null, "AddVenueTeacher"));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult RemovePlacement(int teacherId, int venueId)
        {
            var teacher = TeacherService.GetTeacher(teacherId);
            var venue = VenueService.GetVenue(venueId);

            if (teacher == null || venue == null)
                return Json(false);

            if (!SecurityContext.CurrentUser.CanUpdate(teacher) && !SecurityContext.CurrentUser.CanUpdate(venue))
                return Json(false);

            var placement = venue.TeacherPlacements.FirstOrDefault(p => p.Teacher.Id == teacher.Id);
            VenueService.RemovePlacement(placement);
            WorkUnit.Commit();

            return Json(true);
        }

        public ActionResult LargeMap(int id)
        {
            var venue = VenueService.GetVenue(id);

            return View(new VenueModel(venue));
        }
    }
}
