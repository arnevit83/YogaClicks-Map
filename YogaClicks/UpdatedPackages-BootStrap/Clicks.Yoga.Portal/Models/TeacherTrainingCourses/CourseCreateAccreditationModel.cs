using System;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    [DataContract]
    public class CourseCreateAccreditationModel
    {
        public CourseCreateAccreditationModel()
        {
            AccreditationBodies = new AccreditationBodyChooserModel();
            Style = new StyleSelectorModel();
        }

        [DataMember]
        public string Accreditation { get; set; }

        [DataMember]
        public AccreditationBodyChooserModel AccreditationBodies { get; private set; }

        [DataMember]
        public StyleSelectorModel Style { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(
            TeacherTrainingCourse entity,
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService)
        {
            entity.Accreditation = Accreditation;
            entity.Style = Style.Selected ? styleService.GetStyle(Style.Id) : null;
            AccreditationBodies.PopulateCollection(entity.AccreditationBodies, accreditationBodyService);
        }

        public CourseCreateAccreditationModel PopulateMetadata(
            IAccreditationBodyService accreditationBodyService,
            IStyleService styleService)
        {
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());
            Style.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}