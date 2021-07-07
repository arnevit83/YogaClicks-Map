using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    public class CourseDisplayModel
    {
        public CourseDisplayModel(IList<TeacherTrainingOrganisation> organisations, TeacherTrainingCourse entity, IList<Review> reviews)
        {
            Organisations = new List<OrganisationBasicModel>();
            
            foreach (var org in organisations)
                Organisations.Add(new OrganisationBasicModel(org));

            Organisation = new TeacherTrainingOrganisationModel(entity.Organisation);
            Course = new TeacherTrainingCourseModel(entity);
            RecentReviews = new List<ReviewModel>();

            foreach (var review in reviews.Take(5).OrderByDescending(r => r.CreationTime))
                RecentReviews.Add(new ReviewModel(review));

            if (reviews.Count > 0)
            {
                AverageRating = reviews.Average(r => r.Rating);
                TotalReviews = reviews.Count;
            }
        }

        public IList<OrganisationBasicModel> Organisations { get; set; }

        public TeacherTrainingOrganisationModel Organisation { get; set; }

        public TeacherTrainingCourseModel Course { get; set; }

        public IList<ReviewModel> RecentReviews { get; set; }

        public int TotalReviews { get; set; }

        public decimal AverageRating { get; set; }
    }
}