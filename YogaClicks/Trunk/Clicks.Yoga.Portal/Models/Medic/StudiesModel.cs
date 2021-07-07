using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class StudiesModel
    {
        public StudiesModel(IEnumerable<Study> studies)
        {
            Studies = new List<StudyModel>();

            foreach (var study in studies)
            {
                Studies.Add(new StudyModel(study));
            }
        }

        public IList<StudyModel> Studies { get; private set; }
    }
}