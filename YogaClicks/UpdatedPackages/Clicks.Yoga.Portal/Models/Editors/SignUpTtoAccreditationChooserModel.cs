using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpTtoAccreditationChooserModel : MultipleEntitySelectorModel<AccreditationBody, AccreditationBodyModel>
    {
        public SignUpTtoAccreditationChooserModel() {}

        public SignUpTtoAccreditationChooserModel(IEnumerable<AccreditationBody> entities) : base(entities) { }

        public void PopulateCollection(ICollection<TeacherTrainingOrganisationAccreditation> collection, IAccreditationBodyService accreditationBodies)
        {
            PopulateCollection(collection, id => new TeacherTrainingOrganisationAccreditation(accreditationBodies.GetAccreditationBody(id)), e => e.AccreditationBody);
        }
    }
}