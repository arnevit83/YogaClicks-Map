using System;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class TeacherStep4ViewModel
    {
        public bool IsAccredited { get; set; }

        public string HoursTraining { get; set; }

        public string YearsTeaching { get; set; }

        public SignUpTeacherAccreditationChooserModel AccreditationBodies { get; set; }

        public SignUpQualificationsCollectionEditorModel Qualifications { get; set; }

        public SignUpSchoolCollectionEditorModel Schools { get; set; }

        public bool IsEdit { get; set; }
        
        public TeacherStep4ViewModel()
        {
            AccreditationBodies = new SignUpTeacherAccreditationChooserModel();
            Qualifications = new SignUpQualificationsCollectionEditorModel();
            Schools = new SignUpSchoolCollectionEditorModel();
        }

        public TeacherStep4ViewModel(Teacher teacher, IAccreditationBodyService accreditationBodyService)
        {
            IsAccredited = teacher.IsAccredited;
            HoursTraining = teacher.HoursTraining;
            YearsTeaching = teacher.YearsTeaching;
            AccreditationBodies = new SignUpTeacherAccreditationChooserModel(teacher.Accreditations.Select(e => e.AccreditationBody));
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            Qualifications = new SignUpQualificationsCollectionEditorModel(teacher.Qualifications);
            Schools = new SignUpSchoolCollectionEditorModel(teacher.Schools);   
        }
        public TeacherStep4ViewModel(Teacher teacher, IAccreditationBodyService accreditationBodyService, bool isEdit)
        {
            IsAccredited = teacher.IsAccredited;
            HoursTraining = teacher.HoursTraining;
            YearsTeaching = teacher.YearsTeaching;
            AccreditationBodies = new SignUpTeacherAccreditationChooserModel(teacher.Accreditations.Select(e => e.AccreditationBody));
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            Qualifications = new SignUpQualificationsCollectionEditorModel(teacher.Qualifications);
            Schools = new SignUpSchoolCollectionEditorModel(teacher.Schools);
            IsEdit = isEdit;
        }

        public void PopulateEntity(Teacher teacher, IAccreditationBodyService accreditationBodies, IQualificationService qualificationService, ISchoolService schoolService)
        {
            teacher.IsAccredited = IsAccredited;
            teacher.HoursTraining = HoursTraining;
            teacher.YearsTeaching = YearsTeaching;
            AccreditationBodies.PopulateCollection(teacher.Accreditations, accreditationBodies);
            Qualifications.PopulateCollection(teacher.Qualifications, qualificationService);
            Schools.PopulateCollection(teacher.Schools, schoolService);
        }
    }
}