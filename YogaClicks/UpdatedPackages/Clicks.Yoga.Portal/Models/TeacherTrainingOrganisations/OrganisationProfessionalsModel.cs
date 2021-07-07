using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationProfessionalsModel
    {
        public OrganisationProfessionalsModel(TeacherTrainingOrganisation teacherTrainingOrganisation, List<Teacher> associatedteachers)
        {
            TeacherTrainingOrganisation = new TeacherTrainingOrganisationModel(teacherTrainingOrganisation);
            AssociatedTeachers = new List<TeacherModel>();

            foreach (var teacher in associatedteachers)
            {
                AssociatedTeachers.Add( new TeacherModel(teacher));
            }
        }
        public OrganisationProfessionalsModel(TeacherTrainingOrganisation teacherTrainingOrganisation, List<Teacher> associatedteachers, List<Teacher> faculty)
        {
            TeacherTrainingOrganisation = new TeacherTrainingOrganisationModel(teacherTrainingOrganisation);
            AssociatedTeachers = new List<TeacherModel>();

            foreach (var teacher in associatedteachers)
            {
                AssociatedTeachers.Add(new TeacherModel(teacher));
            }


            Faculty = new List<TeacherModel>();

            foreach (var teacher in faculty)
            {
                Faculty.Add(new TeacherModel(teacher));
            }


        }
        public List<TeacherModel> AssociatedTeachers { get; private set; }

        public List<TeacherModel> Faculty { get; private set; }

        public TeacherTrainingOrganisationModel TeacherTrainingOrganisation { get; private set; }

    }
}