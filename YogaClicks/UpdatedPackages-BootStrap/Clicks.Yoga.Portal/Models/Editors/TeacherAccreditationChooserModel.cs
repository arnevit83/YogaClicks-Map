using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class TeacherAccreditationChooserModel : MultipleEntitySelectorModel<AccreditationBody, AccreditationBodyModel>
    {
        public TeacherAccreditationChooserModel() {}

        public TeacherAccreditationChooserModel(IEnumerable<AccreditationBody> entities) : base(entities) { }

        public void PopulateCollection(ICollection<TeacherAccreditation> collection, IAccreditationBodyService accreditationBodies)
        {
            PopulateCollection(collection, id => new TeacherAccreditation(accreditationBodies.GetAccreditationBody(id)), e => e.AccreditationBody);
        }
    }
}