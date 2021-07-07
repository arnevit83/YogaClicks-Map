using System;
using System.Collections.Generic;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class Review : PrincipalEntity, ICommentable
    {
        public Review()
        {
            ReviewedExperiences = new List<ReviewExperience>();
            DetailedRatings = new List<ReviewRating>();
            Comments = new List<Comment>();
        }

        public virtual EntityHandle AuthorHandle { get; set; }

        public virtual EntityHandle SubjectHandle { get; set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        public decimal Rating { get; set; }

        public virtual ICollection<ReviewExperience> ReviewedExperiences { get; private set; }

        public virtual ICollection<ReviewRating> DetailedRatings { get; private set; }

        public virtual ICollection<Comment> Comments { get; private set; }

        public Type GetCommentPermissionProviderType()
        {
            return typeof(ReviewCommentPermissionProvider);
        }

        public EntityReference? GetReferencedEntity()
        {
            return null;
        }

        public int HelpfulCount { get; set; }
    }
}