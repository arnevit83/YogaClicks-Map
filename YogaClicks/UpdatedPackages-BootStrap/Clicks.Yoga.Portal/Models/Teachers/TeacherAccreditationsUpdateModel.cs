using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherAccreditationsUpdateModel
    {
        public TeacherAccreditationsUpdateModel()
        {
            AccreditationBodies = new TeacherAccreditationChooserModel();
        }

        public TeacherAccreditationsUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            AccreditationBodies = new TeacherAccreditationChooserModel(teacher.Accreditations.Select(e => e.AccreditationBody));
        }

        public void PopulateEntity(
            Teacher entity,
            IAccreditationBodyService accreditationBodies)
        {
            AccreditationBodies.PopulateCollection(entity.Accreditations, accreditationBodies);
        }

        public int TeacherId { get; set; }

        public TeacherAccreditationChooserModel AccreditationBodies { get; set; }

        public TeacherAccreditationsUpdateModel PopulateMetadata(
            Teacher teacher,
            IAccreditationBodyService accreditationBodyService)
        {
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            return this;
        }
    }
}