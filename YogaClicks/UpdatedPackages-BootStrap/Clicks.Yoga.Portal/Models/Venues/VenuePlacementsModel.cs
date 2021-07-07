using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenuePlacementsModel
    {
        public VenuePlacementsModel(Venue venue, IEnumerable<TeacherPlacement> placements)
        {
            Venue = new VenueModel(venue);
            TeacherPlacements = new List<TeacherPlacementModel>();

            foreach (var placement in placements.Where(p => p.Teacher.Active))
            {
                TeacherPlacements.Add(new TeacherPlacementModel(placement));
            }
        }

        public VenueModel Venue { get; private set; }

        public IList<TeacherPlacementModel> TeacherPlacements { get; private set; }
    }
}