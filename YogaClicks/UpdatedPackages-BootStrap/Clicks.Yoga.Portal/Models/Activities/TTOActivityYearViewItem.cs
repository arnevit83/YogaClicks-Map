using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class TTOActivityYearViewItem
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }

        public bool TTOCourse { get; set; }

        public int? VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenueCity { get; set; }
        public string VenueCountry { get; set; }

        public string Description { get; set; }
    }
}