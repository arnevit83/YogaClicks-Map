using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ReviewModel : PrincipalEntityModel<Review>
    {
        public ReviewModel(Review entity) : base(entity) {}

        public EntityHandleModel Author { get; set; }

        public EntityHandleModel Subject { get; set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        public decimal Rating { get; set; }

        public IList<ReviewExperienceModel> ReviewedExperiences { get; private set; } 

        public IList<CommentModel> Comments { get; private set; } 

        public int HelpfulCount { get; set; }

        public override void Populate(Review entity)
        {
            Id = entity.Id;
            Headline = entity.Headline;
            Body = entity.Body;
            Rating = entity.Rating;
            HelpfulCount = entity.HelpfulCount;
            CreationTime = entity.CreationTime;

            Author = new EntityHandleModel(entity.AuthorHandle);
            Subject = new EntityHandleModel(entity.SubjectHandle);

            Comments = new List<CommentModel>();

            foreach (var comment in entity.Comments.Where(c => c.ActorHandle.Active))
            {
                Comments.Add(new CommentModel(comment));
            }

            ReviewedExperiences = new List<ReviewExperienceModel>();

            foreach (var experience in entity.ReviewedExperiences)
            {
                ReviewedExperiences.Add(new ReviewExperienceModel(experience));
            }
        }
    }
}