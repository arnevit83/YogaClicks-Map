using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherDisplayModel
    {
        public TeacherDisplayModel(Teacher teacher, Profile profile, Venue venue, TeacherTrainingOrganisation tto, bool visible)
        {
            Teacher = new TeacherModel(teacher);
            Profile = new ProfileModel(profile);

            if (venue != null)
            {
                Venue = new VenueModel(venue);
            }

            if (tto != null)
            {
                TTO = new TeacherTrainingOrganisationModel(tto);
            }
            IsProfileVisible = visible;
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

        public bool IsProfileVisible { get; set; }
    }
}