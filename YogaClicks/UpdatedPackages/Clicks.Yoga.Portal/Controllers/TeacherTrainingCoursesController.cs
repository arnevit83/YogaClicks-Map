using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
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
              IMedicService medicService,
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
            MedicService = medicService;
        }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IAccreditationBodyService AccreditationBodyService { get; set; }

        private IStyleService StyleService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        private IMedicService MedicService { get; set; }

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

        [HttpGet]
        [YogaAuthorize]
        public ActionResult CreateCourse(int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            var teacher = profile != null ? TeacherService.GetTeacherForUser(profile.Owner.Id) : null;
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;

            var model = new CreateCourseAllSteps(profile, teacher, venue, tto, AccreditationBodyService, StyleService, id, MedicService);
            return View("CreateCourseAllSteps", model);
        }

        [HttpGet]
        [YogaAuthorize]
        public ActionResult CloneCourse(int id,int ttcId)
        {


            Profile profile = this.SecurityContext.CurrentProfile;

            var teacher = profile != null ? TeacherService.GetTeacherForUser(profile.Owner.Id) : null;
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;

            var model = new CreateCourseAllSteps(profile, teacher, venue, tto, AccreditationBodyService, StyleService, id, MedicService);

            if (ttcId != 0)
            {
                var course = TeacherTrainingService.GetCourse(ttcId);
                model.PopulateMetadataClone(course, StyleService, MedicService, TeacherService, VenueService, AccreditationBodyService);
            }

            return View("CreateCourseAllSteps", model);
        }


        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateCourse(CreateCourseAllSteps model)
        {
            var course = new TeacherTrainingCourse { Active = true };
            model.PopulateEntity(course, AccreditationBodyService, StyleService, TeacherTrainingService, WebsiteService, TeacherService, VenueService, MedicService);
            TeacherTrainingService.AddCourse(course);

            WorkUnit.Commit();
            
            return RedirectToAction("Display", new {id = course.Id});
        }

        [YogaAuthorize]
        public ActionResult Create(int organisationId)
        {
            var createModel = new CourseCreateModel();
            Session.Add("createModel",createModel);
         //   ViewBag.CreateModel = createModel;
            return View("CreateBasic", new CourseCreateBasicModel(organisationId));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateBasic(CourseCreateBasicModel model)
        {
          



          //  ViewBag.CreateModel = createModel;
            if (!ModelState.IsValid) return View(model);
            var createModel = (CourseCreateModel)Session["createModel"];
            createModel.BasicModel = model;
            Session.Add("createModel", createModel);
            return View("CreateAccreditation", new CourseCreateAccreditationModel().PopulateMetadata(AccreditationBodyService, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateAccreditation(CourseCreateAccreditationModel model)
        {

            var createModel = (CourseCreateModel)Session["createModel"];

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateBasic", createModel.BasicModel);
            }
            
            if (!ModelState.IsValid) return View(model.PopulateMetadata(AccreditationBodyService, StyleService));

            createModel.AccreditationModel = model;
            Session.Add("createModel", createModel);

        
            return View("CreateTeachers", new CourseCreateTeachersModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateTeachers(CourseCreateTeachersModel model)
        {
            var createModel = (CourseCreateModel)Session["createModel"];

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateAccreditation", createModel.AccreditationModel.PopulateMetadata(AccreditationBodyService, StyleService));
            }

            if (!ModelState.IsValid) return View(model);
            createModel.TeachersModel = model;
            Session.Add("createModel", createModel);
            return View("CreateVenues", new CourseCreateVenuesModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVenues(CourseCreateVenuesModel model)
        {
            var createModel = (CourseCreateModel)Session["createModel"];

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateTeachers", createModel.TeachersModel);
            }

            if (!ModelState.IsValid) return View(model);
            createModel.VenuesModel = model;
            Session.Add("createModel", createModel);
            return View("CreateNotes", new CourseCreateNotesModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateNotes(CourseCreateNotesModel model)
        {
            var createModel = (CourseCreateModel)Session["createModel"];

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
            Session.Remove("createModel");
            return View("CloseModal", new CloseModalModel(Url.Action("teachertrainingcourses", course.Id.ToString()), "CreateTeacherTrainingCourse"));
        }
        
        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var course = TeacherTrainingService.GetCourseForEditing(id);

            return View(new CourseUpdateModel(course).PopulateMetadata(AccreditationBodyService, StyleService, MedicService,course.Conditions));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, CourseUpdateModel model)
        {
            var course = TeacherTrainingService.GetCourseForEditing(id);

            if (!ModelState.IsValid) return View(model.PopulateMetadata(AccreditationBodyService, StyleService, MedicService,course.Conditions));

            model.PopulateEntity(course, TeacherTrainingService, WebsiteService, AccreditationBodyService, StyleService, TeacherService, VenueService, MedicService);

            WorkUnit.Commit();

            return RedirectToAction("Display", new { id = course.Id });
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
