using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherDisplayModel
    {
        public TeacherDisplayModel(Teacher teacher, Profile profile, bool visible)
        {
            Teacher = new TeacherModel(teacher);
            Profile = new ProfileModel(profile);
            IsProfileVisible = visible;
        }

        public TeacherModel Teacher { get; set; }

        public ProfileModel Profile { get; set; }

        public bool IsProfileVisible { get; set; }
    }
}