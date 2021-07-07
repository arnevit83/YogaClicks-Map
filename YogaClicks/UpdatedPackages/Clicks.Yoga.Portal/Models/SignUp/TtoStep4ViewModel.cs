using System;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class TtoStep4ViewModel
    {
        public int Id { get; set; }

        public bool IsEdit { get; set; }

        public bool TherapyTeacherTraining { get; set; }

        public SignUpTtoQualificationsCollectionEditorModel Qualifications { get; set; }

        public SignUpTtoAccreditationChooserModel AccreditationBodies { get; set; }
        
        public TtoStep4ViewModel()
        {
            Qualifications = new SignUpTtoQualificationsCollectionEditorModel();
            AccreditationBodies = new SignUpTtoAccreditationChooserModel();
        }

        public TtoStep4ViewModel(TeacherTrainingOrganisation tto, IAccreditationBodyService accreditationBodyService)
        {
            Id = tto.Id;
            TherapyTeacherTraining = tto.TherapyTeacherTraining;
            AccreditationBodies = new SignUpTtoAccreditationChooserModel(tto.Accreditations.Select(e => e.AccreditationBody));
            var testing = accreditationBodyService.GetAccreditationBodies();
            AccreditationBodies.PopulateMetadata(testing);
            Qualifications = new SignUpTtoQualificationsCollectionEditorModel(tto.Qualifications);
        }

        public TtoStep4ViewModel(TeacherTrainingOrganisation tto, IAccreditationBodyService accreditationBodyService, bool isEdit)
        {
            Id = tto.Id;
            TherapyTeacherTraining = tto.TherapyTeacherTraining;
            AccreditationBodies = new SignUpTtoAccreditationChooserModel(tto.Accreditations.Select(e => e.AccreditationBody));
            var testing = accreditationBodyService.GetAccreditationBodies();
            AccreditationBodies.PopulateMetadata(testing);
            Qualifications = new SignUpTtoQualificationsCollectionEditorModel(tto.Qualifications);
            IsEdit = isEdit;
        }

        public void PopulateEntity(TeacherTrainingOrganisation tto, IAccreditationBodyService accreditationBodies, IQualificationService qualificationService, ISchoolService schoolService)
        {
            tto.TherapyTeacherTraining = TherapyTeacherTraining;
            AccreditationBodies.PopulateCollection(tto.Accreditations, accreditationBodies);
            Qualifications.PopulateCollection(tto.Qualifications, qualificationService);
        }
    }
}