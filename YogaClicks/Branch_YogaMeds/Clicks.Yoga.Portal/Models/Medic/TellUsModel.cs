using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class TellUsModel
    {
        public TellUsModel()
        {
            Teachers = new TellUsTeacherChooserModel();
            Venues = new TellUsVenueChooserModel();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool OwnerHidden { get; set; }

        public int ConditionId { get; set; }

        [DataMember]
        public TellUsTeacherChooserModel Teachers { get; private set; }

        [DataMember]
        public TellUsVenueChooserModel Venues { get; private set; }
    }
}