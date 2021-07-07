using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpTeacherAccreditationChooserModel : MultipleEntitySelectorModel<AccreditationBody, AccreditationBodyModel>
    {
        public SignUpTeacherAccreditationChooserModel() {}

        public SignUpTeacherAccreditationChooserModel(IEnumerable<AccreditationBody> entities) : base(entities) { }

        public void PopulateCollection(ICollection<TeacherAccreditation> collection, IAccreditationBodyService accreditationBodies)
        {
            PopulateCollection(collection, id => new TeacherAccreditation(accreditationBodies.GetAccreditationBody(id)), e => e.AccreditationBody);
        }
    }
}