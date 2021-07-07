using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Shared;
using Clicks.Yoga.Portal.Models.TeacherTrainingCourses;
using Clicks.Yoga.Web;
using Microsoft.Web.Mvc;

namespace Clicks.Yoga.Portal.Controllers
{
    public class TeacherTrainingCoursesController : YogaController
    {
        public TeacherTrainingCoursesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            ITeacherTrainingService teacherTrainingService,
            IReviewService reviewService,
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService,
            IWebsiteService websiteService,
            ITeacherService teacherService,
            IVenueService venueService,
            IImageService imageService)
            : base(workUnit, securityContext)
        {
            TeacherTrainingService = teacherTrainingService;
            ReviewService = reviewService;
            AccreditationBodyService = accreditationBodyService;
            StyleService = styleService;
            WebsiteService = websiteService;
            TeacherService = teacherService;
            VenueService = venueService;
            ImageService = imageService;
        }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IAccreditationBodyService AccreditationBodyService { get; set; }

        private IStyleService StyleService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IVenueService VenueService { get; set; }

        private IImageService ImageService { get; set; }

        public ActionResult Display(int id)
        {
            var course = TeacherTrainingService.GetCourseForDisplay(id);
            var orgs = TeacherTrainingService.GetOrganisations();

            foreach (var result in EnsureUrlSlug(course)) return result;

            var reviews = ReviewService.GetReviewsBySubject(course);

            return View(new CourseDisplayModel(orgs, course, reviews));
        }
        
        [YogaAuthorize]
        public ActionResult Create(int organisationId)
        {
            var createModel = new CourseCreateModel();
            ViewBag.CreateModel = createModel;
            return View("CreateBasic", new CourseCreateBasicModel(organisationId));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateBasic(CourseCreateBasicModel model, [Deserialize] CourseCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;
            if (!ModelState.IsValid) return View(model);
            createModel.BasicModel = model;
            return View("CreateAccreditation", new CourseCreateAccreditationModel().PopulateMetadata(AccreditationBodyService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateAccreditation(CourseCreateAccreditationModel model, [Deserialize] CourseCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateBasic", createModel.BasicModel);
            }
            
            if (!ModelState.IsValid) return View(model.PopulateMetadata(AccreditationBodyService, StyleService));
            createModel.AccreditationModel = model;
            return View("CreateTeachers", new CourseCreateTeachersModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateTeachers(CourseCreateTeachersModel model, [Deserialize] CourseCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateAccreditation", createModel.AccreditationModel.PopulateMetadata(AccreditationBodyService, StyleService));
            }

            if (!ModelState.IsValid) return View(model);
            createModel.TeachersModel = model;
            return View("CreateVenues", new CourseCreateVenuesModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVenues(CourseCreateVenuesModel model, [Deserialize] CourseCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateTeachers", createModel.TeachersModel);
            }

            if (!ModelState.IsValid) return View(model);
            createModel.VenuesModel = model;
            return View("CreateNotes", new CourseCreateNotesModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateNotes(CourseCreateNotesModel model, [Deserialize] CourseCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateVenues", createModel.VenuesModel);
            }

            if (!ModelState.IsValid) return View(model);

            createModel.NotesModel = model;

            var course = new TeacherTrainingCourse { Active = true };
            createModel.PopulateEntity(course, TeacherTrainingService, WebsiteService, AccreditationBodyService, StyleService, TeacherService, VenueService);

            TeacherTrainingService.AddCourse(course);

            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(Url.Action("Display", new { course.Id }), "CreateTeacherTrainingCourse"));
        }
        
        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var course = TeacherTrainingService.GetCourseForEditing(id);

            return View(new CourseUpdateModel(course).PopulateMetadata(AccreditationBodyService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, CourseUpdateModel model)
        {
            var course = TeacherTrainingService.GetCourseForEditing(id);

            if (!ModelState.IsValid) return View(model.PopulateMetadata(AccreditationBodyService, StyleService));

            model.PopulateEntity(course, TeacherTrainingService, WebsiteService, AccreditationBodyService, StyleService, TeacherService, VenueService);

            WorkUnit.Commit();

            return RedirectToAction("Display");
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Delete(int id)
        {
            TeacherTrainingService.DeleteCourse(id);
            WorkUnit.Commit();
            return new EmptyResult();
        }
    }
}
