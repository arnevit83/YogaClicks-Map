using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationDisplayModel
    {
        public OrganisationDisplayModel(TeacherTrainingOrganisation entity)
        {
            Organisation = new TeacherTrainingOrganisationModel(entity);
        }

        public TeacherTrainingOrganisationModel Organisation { get; private set; }
    }
}