using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileNewsFeedItemModel
    {
        public ProfileNewsFeedItemModel(Teacher teacher)
        {
            Name = teacher.Name;
            Description = teacher.Philosophy;
            if (teacher.Image != null) Image = new ImageModel(teacher.Image);
            ActivityTime = teacher.CreationTime;
            Uri = "/teachers/" + teacher.Id;
            IsTeacher = true;
        }

        public ProfileNewsFeedItemModel(Venue venue)
        {
            Name = venue.Name;
            Description = venue.Description;
            if (venue.Image != null) Image = new ImageModel(venue.Image);
            ActivityTime = venue.CreationTime;
            Uri = "/venues/" + venue.Id;
            IsVenue = true;
        }

        public ProfileNewsFeedItemModel(Review review)
        {
            Name = review.SubjectHandle.Name;
            Description = review.Headline;
            Author = review.AuthorHandle.Name;
            if (review.AuthorHandle.Image != null) Image = new ImageModel(review.AuthorHandle.Image);
            ActivityTime = review.CreationTime;
            Uri = "/" + review.SubjectHandle.EntityType.Name + "s/" + review.SubjectHandle.EntityId;
            Rating = review.Rating;
            IsReview = true;
        }

        public bool IsTeacher { get; set; }

        public bool IsVenue { get; set; }

        public bool IsReview { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ImageModel Image { get; set; }

        public decimal? Rating { get; set; }

        public DateTime ActivityTime { get; set; }

        public TimeSpan TimeSpanSince
        {
            get { return DateTime.Now - ActivityTime; }
        }

        public string Uri { get; set; }

        public string TimeSinceString
        {
            get
            {
                var ts = TimeSpanSince;

                if (ts.TotalSeconds < 2)
                    return "1 second ago";
                
                if (ts.TotalSeconds < 60)
                    return ts.TotalSeconds.ToString("N0") + " seconds ago";

                if (ts.TotalMinutes < 2)
                    return "1 minute ago";

                if (ts.TotalMinutes < 60)
                    return ts.TotalMinutes.ToString("N0") + " minutes ago";

                if (ts.TotalHours < 2)
                    return "1 hour ago";

                return "Earlier on";
            }
        }
    }
}
