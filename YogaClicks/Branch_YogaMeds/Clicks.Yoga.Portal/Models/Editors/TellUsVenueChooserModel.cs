using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class TellUsVenueChooserModel : VenueChooserModel
    {
        public TellUsVenueChooserModel() { }

        public TellUsVenueChooserModel(ICollection<Venue> entities) : base(entities) { }
    }
}