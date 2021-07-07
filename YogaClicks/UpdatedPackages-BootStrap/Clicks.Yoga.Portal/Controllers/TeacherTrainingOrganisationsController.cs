using System;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Models.Shared;
using Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations;
using Clicks.Yoga.Web;
using Clicks.Yoga.Portal.Enums.Profiles;
using System.Linq;
using System.Collections.Generic;

namespace Clicks.Yoga.Portal.Controllers
{
    public class TeacherTrainingOrganisationsController : YogaController
    {
        public TeacherTrainingOrganisationsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            ITeacherTrainingService teacherTrainingService,
            IAccountService accountService,
            IAddressService addressService,
            IWebsiteService websiteService,
            IImageService imageService,
            IStyleService styleService,
            IReviewService reviewService,
             IEntityService entityService,
            IActivityService activityService)
            : base(workUnit, securityContext)
        {
            TeacherTrainingService = teacherTrainingService;
            AccountService = accountService;
            AddressService = addressService;
            WebsiteService = websiteService;
            ImageService = imageService;
            StyleService = styleService;
            ReviewService = reviewService;
            EntityService = entityService;
            ActivityService = activityService;
        }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        private IAccountService AccountService { get; set; }

        private IAddressService AddressService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        private IImageService ImageService { get; set; }

        private IStyleService StyleService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IEntityService EntityService { get; set; }

        private IActivityService ActivityService { get; set; }

        public ActionResult Display(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationDisplayModel(organisation));
        }

        public ActionResult Courses(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationCoursesModel(organisation));
        }

        [HttpPost]
        public ActionResult CoursesPartial(TeacherTrainingCourseSearchCriteriaModel parameters)
        {
            var criteria = new TeacherTrainingCourseSearchCriteria();
            parameters.PopulateEntity(criteria);
            var courses = TeacherTrainingService.SearchCourses(criteria);
            return View(new OrganisationCoursesPartialModel(courses));
        }

        public ActionResult FilterableCoursesPartial(int id)
        {
            return View(new TeacherTrainingCourseSearchCriteriaModel { OrganisationId = id, Completed = false });
        }

        public ActionResult Fans(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View("Fans", new OrganisationFansModel(organisation));
        }

        public ActionResult Reviews(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            var statistics = ReviewService.GetStatisticsBySubjectParent(organisation);

            return View(new OrganisationReviewsModel(organisation, statistics));
        }

        public ActionResult Activities(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationActivitiesModel(organisation));
        }

        public ActionResult Classes(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);
            var entityHandle = EntityService.GetEntityHandle(organisation.Id, "TeacherTrainingOrganisation");

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationClassesModel(organisation, entityHandle.Id));
        }

        public ActionResult Schedule(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);
            var entityHandle = EntityService.GetEntityHandle(organisation.Id, "TeacherTrainingOrganisation");

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationClassesModel(organisation, entityHandle.Id));
        }

        public ActionResult Galleries(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationGalleriesModel(organisation));
        }

        public ActionResult Gallery(int id, int galleryId)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationGalleryModel(organisation, galleryId));
        }

        public ActionResult Videos(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationVideosModel(organisation));
        }

        public ActionResult Professionals(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);
            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            var activitiesPromoted = ActivityService.GetActivitiesAndTeachersForPromoter(id);
            var teachersAssociated = new List<Teacher>();

            if (activitiesPromoted.Any())
            {
                foreach (var activity in activitiesPromoted.Where(x => x.FinishTime >= DateTime.Now || x.RepeatFrequency != null))
                {
                    bool singleRepeat = false;
                    if (activity.RepeatFrequency != null)
                    {
                        if (activity.RepeatFrequency.IsSingle)
                        {
                            singleRepeat = true;
                        }
                    }

                    if (activity.Teachers.Any() && !singleRepeat)
                    {
                        foreach (var teacher in activity.Teachers) teachersAssociated.Add(teacher.Teacher);
                    }
                }
            }

            return View(new OrganisationProfessionalsModel(organisation, teachersAssociated.Where(x => !x.Stubbed).Distinct().ToList()));
        }

        public ActionResult Groups(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationGroupsModel(organisation));
        }

        [YogaAuthorize]
        public ActionResult Create(int userId)
        {
            return View(new OrganisationCreateModel(userId));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Create(OrganisationCreateModel model)
        {
            if (!ModelState.IsValid) return View(new OrganisationCreateModel());

            return View("CreateAddress", new OrganisationCreateAddressModel(model).PopulateMetadata(AddressService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateAddress(OrganisationCreateAddressModel model)
        {
            if (model.Back)
            {
                ModelState.Clear();
                return View("Create", model.CreateModel);
            }

            if (!ModelState.IsValid) return View(model.PopulateMetadata(AddressService));

            var organisation = new TeacherTrainingOrganisation();
            model.PopulateEntity(organisation, AccountService, AddressService, WebsiteService);

            TeacherTrainingService.AddOrganisation(organisation);

            WorkUnit.Commit();

            var wizard = WizardStatus.None;

            if (Session["WizardStatus"] != null)
                wizard = (WizardStatus)Session["WizardStatus"];

            return RedirectToAction("CongratulationsProfileCreated", "Profiles", new { id = organisation.Id, profileType = ProfileTypeEnum.TTO });
        }

        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(id);

            return View(new OrganisationUpdateModel(organisation, AddressService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, OrganisationUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(id);

            if (ModelState.IsValid)
            {
                model.PopulateEntity(organisation, AddressService, WebsiteService, ImageService, StyleService);
                organisation.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("Display");
            }

            return View(new OrganisationUpdateModel(organisation, AddressService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateBanner(OrganisationBannerUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(model.EntityId);

            if (ModelState.IsValid)
            {
                model.PopulateEntity(organisation, ImageService);
                organisation.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("Update", model.EntityId);
            }

            return View(new OrganisationUpdateModel(organisation, AddressService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateImage(OrganisationImageUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(model.EntityId);

            if (ModelState.IsValid)
            {
                model.PopulateEntity(organisation, ImageService);
                organisation.ModificationTime = DateTime.Now;
                WorkUnit.Commit();
                return RedirectToAction("Update", model.EntityId);
            }

            return View(new OrganisationUpdateModel(organisation, AddressService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateDetails(OrganisationDetailsUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(model.EntityId);

            model.PopulateEntity(organisation, WebsiteService);
            organisation.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.EntityId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateLocation(OrganisationLocationUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(model.EntityId);

            model.PopulateEntity(organisation, AddressService);
            organisation.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.EntityId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateAbout(OrganisationAboutUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(model.EntityId);

            model.PopulateEntity(organisation);
            organisation.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.EntityId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateStyles(OrganisationStylesUpdateModel model)
        {
            var organisation = TeacherTrainingService.GetOrganisationForEditing(model.EntityId);

            model.PopulateEntity(organisation, StyleService);
            organisation.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.EntityId);
        }
    }
}
