using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationClassesModel
    {
        public OrganisationClassesModel(TeacherTrainingOrganisation teacherTrainingOrganisation, int entityHandleId)
        {
            TeacherTrainingOrganisation = new TeacherTrainingOrganisationModel(teacherTrainingOrganisation);
            EntityHandleId = entityHandleId;
        }

        public TeacherTrainingOrganisationModel TeacherTrainingOrganisation { get; private set; }

        public int EntityHandleId { get; set; }
    }
}