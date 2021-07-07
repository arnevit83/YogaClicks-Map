using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationVideosModel
    {
        public OrganisationVideosModel(TeacherTrainingOrganisation teacherTrainingOrganisation)
        {
            TeacherTrainingOrganisation = new TeacherTrainingOrganisationModel(teacherTrainingOrganisation);
        }

        public TeacherTrainingOrganisationModel TeacherTrainingOrganisation { get; private set; }
    }
}