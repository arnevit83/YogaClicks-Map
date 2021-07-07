using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    public class OrganisationBasicModel
    {
        public OrganisationBasicModel(TeacherTrainingOrganisation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(Name); }
        }
    }
}
