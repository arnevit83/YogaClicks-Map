using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class TtoStep5ViewModel
    {
        public int Id { get; set; }

        public bool IsEdit { get; set; }

        public SignUpStyleChooserModel Styles { get; set; }

        public SignUpConditionChooserModel Conditions { get; set; }

        public string Description { get; set; }

        public TtoStep5ViewModel()
        {
            Styles = new SignUpStyleChooserModel();
            Conditions = new SignUpConditionChooserModel();
        }

        public TtoStep5ViewModel(TeacherTrainingOrganisation tto, IStyleService styleService, IMedicService medicService)
        {
            Id = tto.Id;
            Styles = new SignUpStyleChooserModel(tto.Styles);
            Conditions = new SignUpConditionChooserModel(tto.Conditions);
            Description = tto.Description;

            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
        }

        public TtoStep5ViewModel(TeacherTrainingOrganisation tto, IStyleService styleService, IMedicService medicService, bool isEdit)
        {
            Id = tto.Id;
            Styles = new SignUpStyleChooserModel(tto.Styles);
            Conditions = new SignUpConditionChooserModel(tto.Conditions);
            Description = tto.Description;

            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
            IsEdit = isEdit;

        }

        public void PopulateEntity(TeacherTrainingOrganisation tto, IStyleService styleService, ITeacherService teacherService, IMedicService medicService)
        {
            Styles.PopulateCollection(tto.Styles, styleService);
            Conditions.PopulateCollection(tto.Conditions, medicService);
            tto.Description = Description;
        }
    }
}