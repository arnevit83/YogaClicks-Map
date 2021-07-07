using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class StudyTherapyTypesUpdateModel
    {
        public StudyTherapyTypesUpdateModel() { }

        public StudyTherapyTypesUpdateModel(StudyModel study) 
        {
            Id = study.Id;
            TherapyTypes = study.TherapyTypes;
        }

        public int Id { get; set; }

        public List<TherapyTypeModel> TherapyTypes { get; set; }
    }
}