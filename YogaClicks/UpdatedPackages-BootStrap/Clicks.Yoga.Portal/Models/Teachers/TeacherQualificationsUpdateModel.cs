using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherQualificationsUpdateModel
    {
        public TeacherQualificationsUpdateModel()
        {
        }

        public TeacherQualificationsUpdateModel(Teacher teacher)
        {
            Qualifications = new QualificationsCollectionEditorModel(teacher.Qualifications);
            TeacherId = teacher.Id;
        }

        public void PopulateEntity(
            Teacher entity,
            IQualificationService qualificationService)
        {
            Qualifications.PopulateCollection(entity.Qualifications, qualificationService);
        }

        public int TeacherId { get; set; }

        public QualificationsCollectionEditorModel Qualifications { get; set; }
    }
}