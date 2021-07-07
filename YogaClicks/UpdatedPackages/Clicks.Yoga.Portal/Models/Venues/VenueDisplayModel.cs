using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueDisplayModel
    {
        public VenueDisplayModel(Venue venue, Profile profile, IList<Review> reviews, bool userHasTeacher, Teacher teacher, TeacherTrainingOrganisation tto)
        {
            Venue = new VenueModel(venue);
            RecentReviews = new List<ReviewModel>();
            if (profile != null) Profile = new ProfileModel(profile);

            foreach (var review in reviews.Take(5).OrderByDescending(r => r.CreationTime))
                RecentReviews.Add(new ReviewModel(review));

            if (reviews.Count > 0)
            {
                AverageRating = reviews.Average(r => r.Rating);
                TotalReviews = reviews.Count;
            }

            CurrentUserHasTeacher = userHasTeacher;


            if (teacher != null)
            {
                Teacher = new TeacherModel(teacher);
            }
            
            if (tto != null)
            {
                TTO = new TeacherTrainingOrganisationModel(tto);
            }
        }

        public decimal AverageRating { get; set; }

        public int TotalReviews { get; set; }
        
        public TeacherModel Teacher { get; set; }

        public VenueModel Venue { get; set; }

        public TeacherTrainingOrganisationModel TTO { get; set; }

        public bool HasTeacher
        {
            get { return Teacher != null; }
        }

        public bool HasVenue
        {
            get { return Venue != null; }
        }

        public bool HasTTO
        {
            get { return TTO != null; }
        }

        public ProfileModel Profile { get; set; }

        public IList<ReviewModel> RecentReviews { get; set; }

        public bool CurrentUserHasTeacher { get; set; }
    }
}