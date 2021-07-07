using System.Collections.Generic;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;
using TeacherService = Clicks.Yoga.Domain.Entities.TeacherService;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpTeacherServiceChooserModel : MultipleEntitySelectorModel<TeacherService, TeacherServiceModel>
    {
        public SignUpTeacherServiceChooserModel() {}

        public SignUpTeacherServiceChooserModel(IEnumerable<TeacherService> entities) : base(entities) { }

        public void PopulateCollection(ICollection<TeacherService> collection, ITeacherService teacherService)
        {
            PopulateCollection(collection, teacherService.GetTeacherService);
        }
    }
}