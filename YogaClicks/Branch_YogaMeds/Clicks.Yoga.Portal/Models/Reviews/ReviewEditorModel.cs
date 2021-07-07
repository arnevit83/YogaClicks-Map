using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewEditorModel
    {
        public ReviewEditorModel()
        {
            HelperDetails = new List<Tuple<string, string>>();
            ReviewableExperiences = new List<ReviewExperienceModel>();
            DetailedRatingSubjects = new List<ReviewRatingSubjectModel>();
            Experiences = new EntityValueDictionary<bool>();
            DetailedRatings = new EntityValueDictionary<decimal>();
        }

        public ReviewEditorModel(IReviewable subject) : this()
        {
            foreach (var detail in subject.GetReviewHelperDetails())
            {
                HelperDetails.Add(detail);
            }
        }

        public ReviewEditorModel(Review review, IReviewable subject) : this(subject)
        {
            ReviewId = review.Id;
            Author = new EntityReference(review.AuthorHandle.EntityId, review.AuthorHandle.EntityType.Name);
            Subject = new EntityHandleModel(review.SubjectHandle);
            Headline = review.Headline;
            Body = review.Body;
            Rating = review.Rating;

            foreach (var experience in review.ReviewedExperiences)
            {
                Experiences.Add(experience.Id, true);
            }

            foreach (var rating in review.DetailedRatings)
            {
                DetailedRatings.Add(rating.Subject.Id, rating.Score);
            }
        }

        public void PopulateEntity(Review review, IReviewService reviewService)
        {
            review.Headline = Headline;
            review.Body = Body;
            review.Rating = Rating;

            foreach (var id in Experiences.Where(p => p.Value).Select(p => Convert.ToInt32(p.Key)))
            {
                if (review.ReviewedExperiences.Any(e => e.Id == id)) continue;

                var experience = reviewService.GetReviewExperience(id);
                review.ReviewedExperiences.Add(experience);
            }

            foreach (var experience in review.ReviewedExperiences.ToList())
            {
                if (!Experiences[experience.Id])
                {
                    review.ReviewedExperiences.Remove(experience);
                }
            }

            foreach (var pair in DetailedRatings)
            {
                var subjectId = Convert.ToInt32(pair.Key);
                var score = pair.Value;
                var rating = review.DetailedRatings.FirstOrDefault(e => e.Subject.Id == subjectId);

                if (score > 0)
                {
                    if (rating == null)
                    {
                        var subject = reviewService.GetReviewRatingSubject(subjectId);
                        review.DetailedRatings.Add(new ReviewRating { Subject = subject, Score = score });
                    }
                    else
                    {
                        rating.Score = score;
                    }
                }
                else
                {
                    review.DetailedRatings.Remove(rating);
                }
            }

            foreach (var rating in review.DetailedRatings)
            {
                if (DetailedRatings[rating.Subject.Id] == 0)
                {
                    review.DetailedRatings.Remove(rating);
                }
            }
        }

        public string Destination { get; set; }

        public int ReviewId { get; set; }

        public EntityReference Author { get; set; }

        public EntityHandleModel Subject { get; set; }

        public IList<Tuple<string, string>> HelperDetails { get; private set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        [UIHint("Rating")]
        public decimal Rating { get; set; }

        public IDictionary<object, bool> Experiences { get; private set; }

        public IDictionary<object, decimal> DetailedRatings { get; private set; }

        public IList<ReviewExperienceModel> ReviewableExperiences { get; private set; }

        public IList<ReviewRatingSubjectModel> DetailedRatingSubjects { get; private set; }

        public bool TermsAccepted { get; set; }

        public ReviewEditorModel PopulateMetadata(IReviewService reviewService, IReviewable reviewable)
        {
            foreach (var experience in reviewService.GetReviewExperiences(reviewable))
            {
                ReviewableExperiences.Add(new ReviewExperienceModel(experience));

                if (!Experiences.ContainsKey(experience.Id))
                {
                    Experiences.Add(experience.Id, false);
                }
            }

            foreach (var subject in reviewService.GetReviewRatingSubjects(reviewable))
            {
                DetailedRatingSubjects.Add(new ReviewRatingSubjectModel(subject));
                if (!DetailedRatings.ContainsKey(subject.Id))
                {
                    DetailedRatings.Add(subject.Id, 0m);
                }
            }

            return this;
        }
    }
}