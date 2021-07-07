using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    public class CourseBasicModel
    {
        public CourseBasicModel(TeacherTrainingCourse course)
        {
            Id = course.Id;
            Name = course.Name;
            OrganisationName = course.Organisation.Name;
            if (course.Image != null) Image = new ImageModel(course.Image);
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string OrganisationName { get; set; }

        public ImageModel Image { get; set; }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(Name); }
        }
    }
}