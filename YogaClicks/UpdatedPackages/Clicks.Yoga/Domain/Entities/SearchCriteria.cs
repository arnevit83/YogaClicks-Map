using System;
using System.Collections.Generic;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Entities
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            EntityTypeIds = new List<int>();
        }

        public int? ProfileId { get; set; }

        public IList<int> EntityTypeIds { get; set; }

        public string Keywords { get; set; }

        public int? StyleId { get; set; }

        public int? ConditionId { get; set; }

        public int? TeacherId { get; set; }

        public int? TeacherServiceId { get; set; }

        public int? VenueId { get; set; }

        public int? VenueTypeId { get; set; }

        public int? VenueServiceId { get; set; }

        public int? AccreditationBodyId { get; set; }

        public int? TeacherPlacementId { get; set; }

        public string Location { get; set; }

        public GeographicPoint Coordinates { get; set; }

        public SearchBounds Bounds { get; set; }

        public DateTime? Date { get; set; }

        public bool? Owned { get; set; }

        public bool? Stubbed { get; set; }

        public SearchSortOrder SortOrder { get; set; }

        public int? PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool? IsAccredited { get; set; }

    }

    public enum SearchSortOrder
    {
        Default,
        Distance,
        Relevance,
        Name
    }
}