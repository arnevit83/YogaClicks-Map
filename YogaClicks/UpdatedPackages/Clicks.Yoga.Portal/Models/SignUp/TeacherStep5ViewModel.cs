using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Teachers;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class TeacherStep5ViewModel
    {
        public int TeacherId { get; set; }

        public SignUpTeacherServiceChooserModel Services { get; set; }

        public SignUpStyleChooserModel Styles { get; set; }

        public SignUpConditionChooserModel Conditions { get; set; }

        public string Philosophy { get; set; }

        public bool IsEdit { get; set; }

        public TeacherStep5ViewModel()
        {
            Services = new SignUpTeacherServiceChooserModel();
            Styles = new SignUpStyleChooserModel();
            Conditions = new SignUpConditionChooserModel();
        }

        public TeacherStep5ViewModel(Teacher teacher)
        {
            Services = new SignUpTeacherServiceChooserModel(teacher.Services);
            Styles = new SignUpStyleChooserModel(teacher.Styles);
            Conditions = new SignUpConditionChooserModel(teacher.Conditions);
            Philosophy = teacher.Philosophy;
        }

        public void PopulateEntity(Teacher teacher, IStyleService styleService, ITeacherService teacherService, IMedicService medicService)
        {
            Services.PopulateCollection(teacher.Services, teacherService);
            Styles.PopulateCollection(teacher.Styles, styleService);
            Conditions.PopulateCollection(teacher.Conditions, medicService);
            teacher.Philosophy = Philosophy;
        }
        
        public void PopulateMetadata(IStyleService styleService, ITeacherService teacherService, IMedicService medicService)
        {
            //Populate the lists with data from the DB
            Services.PopulateMetadata(teacherService.GetTeacherServices());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
        }
        public void PopulateMetadata(IStyleService styleService, ITeacherService teacherService, IMedicService medicService, bool isEdit)
        {
            //Populate the lists with data from the DB
            Services.PopulateMetadata(teacherService.GetTeacherServices());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
            IsEdit = isEdit;
        }
    }
}