using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateTeacherLocationModel
    {
        public LocationEditorModel Location { get; set; }

        public void PopulateEntity(Teacher entity)
        {
            Location.PopulateEntity(entity.Location);
        }
    }
}