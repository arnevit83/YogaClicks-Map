using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileDisplayModel
    {
        public ProfileDisplayModel(Profile profile, Teacher teacher, Venue venue, TeacherTrainingOrganisation tto, bool visible, IEnumerable<Image> images)
        {
            Profile = new ProfileModel(profile);
            Visible = visible;
            Images = new List<ImageModel>();

            if (teacher != null)
            {
                Teacher = new TeacherModel(teacher);
            }

            if (venue != null)
            {
                Venue = new VenueModel(venue);
            }

            if (tto != null)
            {
                TTO = new TeacherTrainingOrganisationModel(tto);
            }

            foreach (var image in images)
            {
                Images.Add(new ImageModel(image));
            }
        }

        public ProfileModel Profile { get; set; }

        public TeacherModel Teacher { get; set; }

        public VenueModel Venue { get; set; }

        public TeacherTrainingOrganisationModel TTO { get; set; }

        public bool HasTeacher
        {
            get { return Teacher != null; }
        }

        public bool HasVenue
        {
            get { return Venue != null; }
        }

        public bool HasTTO
        {
            get { return TTO != null; }
        }

        public bool Visible { get; set; }

        public IList<ImageModel> Images { get; private set; }
    }
}