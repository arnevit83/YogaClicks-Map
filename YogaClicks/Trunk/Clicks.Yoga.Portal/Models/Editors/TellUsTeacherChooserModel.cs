using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class TellUsTeacherChooserModel : TeacherChooserModel
    {
        public TellUsTeacherChooserModel() { }

        public TellUsTeacherChooserModel(ICollection<Teacher> entities) : base(entities) { }
    }
}