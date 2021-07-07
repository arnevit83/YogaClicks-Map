using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Emails;

namespace Clicks.Yoga.Domain.Services
{
    public class ReviewService : ServiceBase, IReviewService
    {
        public ReviewService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            ISearchService searchService,
            INewsService newsService,
            INotificationService notificationService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            SearchService = searchService;
            NewsService = newsService;
            NotificationService = notificationService;
        }

        private IEntityService EntityService { get; set; }

        private ISearchService SearchService { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        public IList<ReviewExperience> GetReviewExperiences(IReviewable subject)
        {
            var typeName = subject.GetEntityTypeName();
            return subject.FilterReviewDetailTypes(EntityContext.ReviewExperiences.Where(e => e.EntityType.Name == typeName).OrderBy(e => e.Name).ToList()).ToList();
        }

        public IList<ReviewExperience> GetReviewExperiences(ReviewSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (!criteria.Any) return new List<ReviewExperience>();

            var query = EntityContext.ReviewExperiences.AsQueryable();

            if (criteria.Subject != null)
            {
                var handle = EntityService.GetEntityHandle(criteria.Subject);
                if (handle == null) return new List<ReviewExperience>();
                query = query.Where(e => e.Reviews.Any(r => r.Active && r.SubjectHandle.Id == handle.Id));
            }

            if (criteria.Parent != null)
            {
                var handle = EntityService.GetEntityHandle(criteria.Parent);
                if (handle == null) return new List<ReviewExperience>();
                query = query.Where(e => e.Reviews.Any(r => r.Active && r.SubjectHandle.Parent.Id == handle.Id));
            }

            if (criteria.Author != null)
            {
                var handle = EntityService.GetEntityHandle(criteria.Author);
                if (handle == null) return new List<ReviewExperience>();
                query = query.Where(e => e.Reviews.Any(r => r.Active && r.AuthorHandle.Id == handle.Id));
            }

            return query.ToList();
        }

        public IList<ReviewRatingSubject> GetReviewRatingSubjects(IReviewable subject)
        {
            var typeName = subject.GetEntityTypeName();
            return subject.FilterReviewDetailTypes(EntityContext.ReviewRatingSubjects.Where(e => e.EntityType.Name == typeName).OrderBy(e => e.Name).ToList()).ToList();
        }

        public IList<Review> GetReviewsBySubject(IReviewable entity)
        {
            if (entity == null) return new List<Review>();
            var handle = EntityService.GetEntityHandle(entity);
            if (handle == null || !handle.Active) return new List<Review>();
            return EntityContext.Reviews.Where(e => e.SubjectHandle.Id == handle.Id).ToList();
        }

        public IList<Review> GetReviews(ReviewSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var query = GetReviewQuery(criteria);

            if (query == null) return new List<Review>();

            switch (criteria.SortOrder)
            {
                case ReviewSortOrder.MostRecent:
                    query = query.OrderByDescending(e => e.CreationTime);
                    break;
                case ReviewSortOrder.MostHelpful:
                    query = query.OrderByDescending(e => e.HelpfulCount).ThenByDescending(e => e.CreationTime);
                    break;
                case ReviewSortOrder.HighestRated:
                    query = query.OrderByDescending(e => e.Rating).ThenByDescending(e => e.CreationTime);
                    break;
            }

            return query.ToList();
        }

        public ReviewExperience GetReviewExperience(int id)
        {
            return EntityContext.ReviewExperiences.Get(id);
        }

        public ReviewRatingSubject GetReviewRatingSubject(int id)
        {
            return EntityContext.ReviewRatingSubjects.Get(id);
        }

        public Review GetReview(int id)
        {
            return EntityContext.Reviews.Get(id);
        }

        public Review GetReviewForDisplay(int id)
        {
            var review = GetReview(id);

            if (review == null) throw new UnknownEntityException();

            return review;
        }

        public Review GetReviewForEditing(int id)
        {
            var review = GetReviewForDisplay(id);

            if (!SecurityContext.CanUpdate(review)) throw new PermissionDeniedException();

            return review;
        }

        public ReviewStatistics GetStatisticsBySubject(IReviewable entity)
        {
            if (entity == null) return new ReviewStatistics(0, 0);

            var handle = EntityService.GetEntityHandle(entity);

            if (handle == null) return new ReviewStatistics(0, 0);

            return GetStatistics(e => e.SubjectHandle.Id == handle.Id);
        }

        public ReviewStatistics GetStatisticsBySubjectParent(IEntityHandle entity)
        {
            if (entity == null) return new ReviewStatistics(0, 0);

            var handle = EntityService.GetEntityHandle(entity);

            if (handle == null) return new ReviewStatistics(0, 0);

            return GetStatistics(e => e.SubjectHandle.Parent.Id == handle.Id);
        }

        public ReviewStatistics GetStatistics(ReviewSearchCriteria criteria)
        {
            var query = GetReviewQuery(criteria);

            if (query == null) return new ReviewStatistics(0, 0);

            return GetStatistics(query);
        }

        public void AddReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException("review");
            if (SecurityContext.IsOwner(review.SubjectHandle))
                throw new PermissionDeniedException();

            EntityContext.Reviews.Add(review);

            var subject = EntityService.GetEntity<IReviewable>(review.SubjectHandle);
            var author = EntityService.GetEntity<IActor>(review.AuthorHandle);

            if (subject.Owner != null) NotificationService.Notify(subject.Owner, "ReviewCreated", author, subject);

            NewsService.PostAction(NewsStoryType.FriendAddedReview, author, subject, review);
        }

        public void ReceiveFeedback(Review review, User user, bool helpful)
        {
            if (review == null)
                throw new ArgumentNullException("review");
            if (user == null)
                throw new ArgumentNullException("user");

            var response = EntityContext.ReviewFeedbackResponses.Get(e => e.Review.Id == review.Id && e.User.Id == user.Id);

            if (response == null)
            {
                response = new ReviewFeedbackResponse { Review = review, User = user };
                EntityContext.ReviewFeedbackResponses.Add(response);
            }

            if (helpful && !response.Helpful)
            {
                review.HelpfulCount += response.IsTransient ? 1 : 2;
            }
            else if (!helpful && response.Helpful)
            {
                review.HelpfulCount -= response.IsTransient ? 1 : 2;
            }

            response.Helpful = helpful;
        }

        public void Report(int reviewId, string reason, string explanation)
        {
            if (string.IsNullOrEmpty(reason))
                throw new ArgumentException("Reason must be specified.", "reason");

            var review = EntityContext.Reviews.Get(e => e.Id == reviewId);

            if (review == null)
                throw new ArgumentException("Invalid review id.", "reviewId");

            var email = new AdminReviewReportEmail(review, reason, explanation);

            email.Send();
        }

        private ReviewStatistics GetStatistics(Expression<Func<Review, bool>> predicate)
        {
            var query = EntityContext.Reviews.Where(predicate).AsQueryable();
            return GetStatistics(query);
        }

        private ReviewStatistics GetStatistics(IQueryable<Review> query)
        {
            return new ReviewStatistics(query.Average(r => (decimal?)r.Rating) ?? 0, query.Count());
        }

        private IQueryable<Review> GetReviewQuery(ReviewSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (!criteria.Any) return null;

            var query = EntityContext.Reviews
                .Include(e => e.Comments)
                .Where(e => e.AuthorHandle.Active)
                .Where(e => e.SubjectHandle.Active);

            if (criteria.Subject != null)
            {
                var handle = EntityService.GetEntityHandle(criteria.Subject);
                if (handle == null) return null;
                query = query.Where(e => e.SubjectHandle.Id == handle.Id);
            }

            if (criteria.Parent != null)
            {
                var handle = EntityService.GetEntityHandle(criteria.Parent);
                if (handle == null) return null;
                query = query.Where(e => e.SubjectHandle.Parent.Id == handle.Id);
            }

            if (criteria.Author != null)
            {
                var handle = EntityService.GetEntityHandle(criteria.Author);
                if (handle == null) return null;
                query = query.Where(e => e.AuthorHandle.Id == handle.Id);
            }

            foreach (var experience in criteria.Experiences)
            {
                query = query.Where(r => r.ReviewedExperiences.Any(e => e.Id == experience.Id));
            }

            return query;
        }
    }
}