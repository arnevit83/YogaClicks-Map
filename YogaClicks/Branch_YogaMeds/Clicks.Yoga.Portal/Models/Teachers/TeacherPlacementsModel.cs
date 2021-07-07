using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherPlacementsModel
    {
        public TeacherPlacementsModel()
        {
            Placements = new List<TeacherPlacementModel>();
        }

        public TeacherPlacementsModel(Teacher teacher)
            : this()
        {
            Teacher = new TeacherModel(teacher);

            foreach (var placement in teacher.Placements.Where(e => e.Venue.Active))
            {
                Placements.Add(new TeacherPlacementModel(placement));
            }
        }

        public TeacherModel Teacher { get; set; }

        public IList<TeacherPlacementModel> Placements { get; private set; }
    }
}