using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class UpdateStudyModel
    {
        public UpdateStudyModel() { }

        public UpdateStudyModel(Study study, int conditionId)
        {
            Study = new StudyModel(study);
            ConditionId = conditionId;
        }

        public int ConditionId { get; set; }

        public StudyModel Study { get; set; }
    }
}