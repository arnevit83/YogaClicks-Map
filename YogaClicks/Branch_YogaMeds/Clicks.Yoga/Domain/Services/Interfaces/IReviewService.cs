using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IReviewService
    {
        IList<ReviewExperience> GetReviewExperiences(IReviewable subject);
        IList<ReviewExperience> GetReviewExperiences(ReviewSearchCriteria criteria);
        IList<ReviewRatingSubject> GetReviewRatingSubjects(IReviewable subject);
        IList<Review> GetReviewsBySubject(IReviewable entity);
        IList<Review> GetReviews(ReviewSearchCriteria criteria);
        ReviewExperience GetReviewExperience(int id);
        ReviewRatingSubject GetReviewRatingSubject(int id);
        Review GetReview(int id);
        Review GetReviewForDisplay(int id);
        Review GetReviewForEditing(int id);
        ReviewStatistics GetStatisticsBySubject(IReviewable entity);
        ReviewStatistics GetStatisticsBySubjectParent(IEntityHandle entity);
        ReviewStatistics GetStatistics(ReviewSearchCriteria criteria);
        void AddReview(Review review);
        void ReceiveFeedback(Review review, User user, bool helpful);
        void Report(int reviewId, string reason, string explanation);
    }
}