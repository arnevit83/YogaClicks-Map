using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationDisplayModel
    {
        public OrganisationDisplayModel(TeacherTrainingOrganisation entity, Teacher teacher, Venue venue)
        {
            Organisation = new TeacherTrainingOrganisationModel(entity);

            if (venue != null)
            {
                Venue = new VenueModel(venue);
            }

            if (teacher != null)
            {
                Teacher = new TeacherModel(teacher);
            }
        }

        public TeacherTrainingOrganisationModel Organisation { get; private set; }

        public TeacherModel Teacher { get; set; }

        public VenueModel Venue { get; set; }

        public bool HasTeacher
        {
            get { return Teacher != null; }
        }

        public bool HasVenue
        {
            get { return Venue != null; }
        }

    }
}