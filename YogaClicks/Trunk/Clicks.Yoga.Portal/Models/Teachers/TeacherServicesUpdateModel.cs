using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherServicesUpdateModel
    {
         public TeacherServicesUpdateModel()
        {
            Services = new TeacherServiceChooserModel();
        }

         public TeacherServicesUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            Services = new TeacherServiceChooserModel(teacher.Services);
        }

        public void PopulateEntity(
            Teacher entity,
            ITeacherService teacherService)
        {
            Services.PopulateCollection(entity.Services, teacherService);
        }

        public int TeacherId { get; set; }

        public TeacherServiceChooserModel Services { get; set; }

        public TeacherServicesUpdateModel PopulateMetadata(
            Teacher teacher,
            ITeacherService teacherService)
        {
            Services.PopulateMetadata(teacherService.GetTeacherServices());
            return this;
        }
    }
}