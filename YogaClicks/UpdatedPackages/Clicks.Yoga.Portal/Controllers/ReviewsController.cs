using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Reviews;
using Clicks.Yoga.Portal.Models.Shared;

namespace Clicks.Yoga.Portal.Controllers
{
    [YogaAuthorize]
    public class ReviewsController : YogaController
    {
        public ReviewsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IEntityService entityService,
            IReviewService reviewService,
            ITeacherService teacherService,
            IVenueService venueService)
            : base(workUnit, securityContext)
        {
            EntityService = entityService;
            ReviewService = reviewService;
            TeacherService = teacherService;
            VenueService = venueService;
        }

        private IEntityService EntityService { get; set; }

        private IReviewService ReviewService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IVenueService VenueService { get; set; }

        private IReviewable Subject { get; set; }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [AllowAnonymous]
        public ActionResult FilterableListPartial(ReviewSearchCriteriaModel parameters)
        {
            var criteria = new ReviewSearchCriteria();
            parameters.PopulateEntity(criteria, ReviewService, EntityService);
            var experiences = ReviewService.GetReviewExperiences(criteria);
            parameters.PopulateMetadata(experiences);
            return PartialView(parameters);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [AllowAnonymous]
        public ActionResult ListPartial(ReviewSearchCriteriaModel parameters)
        {
            var criteria = new ReviewSearchCriteria();
            parameters.PopulateEntity(criteria, ReviewService, EntityService);
            var reviews = ReviewService.GetReviews(criteria);
            var showSubject = criteria.Author != null;
            var showChild = criteria.Parent != null;
            return PartialView("ListPartial", new ReviewListPartialModel(reviews, showSubject, showChild));
        }

        [AllowAnonymous]
        public ActionResult SummaryPartial(ReviewSearchCriteriaModel parameters)
        {
            var criteria = new ReviewSearchCriteria();
            parameters.PopulateEntity(criteria, ReviewService, EntityService);
            var statistics = ReviewService.GetStatistics(criteria);
            var subject = parameters.SubjectId.HasValue ? EntityService.GetEntity<IReviewable>(parameters.SubjectId.Value, parameters.SubjectType) : null;
            return PartialView("SummaryPartial", new ReviewSummaryPartialModel(parameters, statistics, subject));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [AllowAnonymous]
        public ActionResult RatingPartial(ReviewSearchCriteriaModel parameters)
        {
            var criteria = new ReviewSearchCriteria();
            parameters.PopulateEntity(criteria, ReviewService, EntityService);
            var statistics = ReviewService.GetStatistics(criteria);
            return PartialView("RatingPartial", new ReviewRatingPartialModel(statistics));
        }

        
        public ActionResult Display(int id)
        {
            var review = ReviewService.GetReviewForDisplay(id);
            return View(new ReviewDisplayModel(review));
        }

        
        [YogaAuthorize]
        public ActionResult Create(int subjectId, string subjectType, string destination)
        {
            Subject = EntityService.GetEntity<IReviewable>(subjectId, subjectType);

            if (!SecurityContext.HasActor || Subject == null) throw new PermissionDeniedException();

            var model = (ReviewCreateModel)new ReviewCreateModel(SecurityContext.CurrentActor, Subject).PopulateMetadata(ReviewService, Subject);

            model.Destination = destination;

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Create(ReviewCreateModel model)
        {
            Subject = EntityService.GetEntity<IReviewable>(model.Subject.Id, model.Subject.EntityType.Name);

            if (!SecurityContext.HasActor || Subject == null) throw new PermissionDeniedException();

            if (!ModelState.IsValid) return View((ReviewCreateModel)model.PopulateMetadata(ReviewService, Subject));

            var review = new Review();

            review.Owner = SecurityContext.CurrentUser;
            review.AuthorHandle = EntityService.EnsureEntityHandle(SecurityContext.CurrentActor);
            review.SubjectHandle = EntityService.EnsureEntityHandle(Subject);

            model.PopulateEntity(review, ReviewService);

            ReviewService.AddReview(review);
            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(null, "CreateReview"));
        }

        
        [YogaAuthorize]
        public ActionResult Update(int id, string destination)
        {
            var review = ReviewService.GetReviewForEditing(id);

            Subject = EntityService.GetEntity<IReviewable>(review.SubjectHandle);

            var model = (ReviewUpdateModel)new ReviewUpdateModel(review, Subject).PopulateMetadata(ReviewService, Subject);

            model.Subject.Id = id;
            model.Destination = destination;

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(ReviewUpdateModel model)
        {
            var review = ReviewService.GetReviewForEditing(model.ReviewId);
            
            Subject = EntityService.GetEntity<IReviewable>(review.SubjectHandle);

            if (!ModelState.IsValid) return View((ReviewUpdateModel)model.PopulateMetadata(ReviewService, Subject));

            model.PopulateEntity(review, ReviewService);
            WorkUnit.Commit();

            return View("CloseModal");
        }

        [YogaAuthorize]
        public ActionResult Feedback(int id, bool helpful)
        {
            var review = ReviewService.GetReviewForDisplay(id);

            ReviewService.ReceiveFeedback(review, SecurityContext.CurrentUser, helpful);
            WorkUnit.Commit();

            return PartialView(new ReviewFeedbackModel(review));
        }

        public ActionResult Report(int id, string reason)
        {
            return View(new ReviewReportModel(id, reason));
        }

        [HttpPost]
        public ActionResult Report(ReviewReportModel model)
        {
            if (!ModelState.IsValid) return View(model);

            ReviewService.Report(model.ReviewId, model.Reason, model.Explanation);
            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(null, "ReportReview"));
        }

        public ActionResult ReportPolicy()
        {
            return View();
        }
    }
}