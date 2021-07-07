using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherConditionsUpdateModel
    {
        public TeacherConditionsUpdateModel()
        {
            Conditions = new ConditionChooserModel();
        }

        public TeacherConditionsUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            Conditions = new ConditionChooserModel(teacher.Conditions);
        }

        public void PopulateEntity(
           Teacher entity,
            IMedicService medicService)
        {
            Conditions.PopulateCollection(entity.Conditions, medicService);
        }

        public int TeacherId { get; set; }

        public ConditionChooserModel Conditions { get; set; }

        public TeacherConditionsUpdateModel PopulateMetadata(
            Teacher teacher,
            IMedicService medicService)
        {
            Conditions.PopulateMetadata(medicService.GetConditions());
            return this;
        }
    }
}