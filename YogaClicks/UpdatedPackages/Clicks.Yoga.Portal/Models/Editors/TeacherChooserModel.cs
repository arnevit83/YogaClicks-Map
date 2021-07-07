using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class TeacherChooserModel : MultipleEntitySelectorModel<Teacher, TeacherModel>
    {
        public TeacherChooserModel() { }

        public TeacherChooserModel(ICollection<Teacher> entities) : base(entities)
        {
            PopulateMetadata(entities);
        }

        public LocationEditorModel Location { get; private set; }

        public void PopulateCollection(ICollection<Teacher> collection, ITeacherService service)
        {
            PopulateCollection(collection, service.GetTeacher);
        }
    }
}