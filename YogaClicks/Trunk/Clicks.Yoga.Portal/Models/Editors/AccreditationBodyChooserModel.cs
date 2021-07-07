using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class AccreditationBodyChooserModel : MultipleEntitySelectorModel<AccreditationBody, AccreditationBodyModel>
    {
        public AccreditationBodyChooserModel() { }

        public AccreditationBodyChooserModel(IEnumerable<AccreditationBody> entities) : base(entities) { }

        public void PopulateCollection(ICollection<AccreditationBody> collection, IAccreditationBodyService accreditationBodies)
        {
            PopulateCollection(collection, accreditationBodies.GetAccreditationBody);
        }
    }
}