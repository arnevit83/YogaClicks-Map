using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Geography;
using Clicks.Yoga.Portal.Models.Editors;
using Newtonsoft.Json;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchCriteriaModel
    {
        public SearchCriteriaModel()
        {
            Date = new MonthEditorModel();
            PageSize = 29;
        }

        public string Types { get; set; }

        public string Keywords { get; set; }

        public bool Stubbed { get; set; }

        public int? Style { get; set; }

        public int? Teacher { get; set; }

        public int? TeacherService { get; set; }

        public int? Venue { get; set; }

        public int? VenueType { get; set; }

        public int? VenueService { get; set; }

        public int? AccreditationBody { get; set; }

        public string Location { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public decimal? NorthBound { get; set; }

        public decimal? SouthBound { get; set; }

        public decimal? EastBound { get; set; }

        public decimal? WestBound { get; set; }

        public MonthEditorModel Date { get; private set; }

        public SearchSortOrder SortOrder { get; set; }

        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public decimal Radius { get; set; }

        [JsonIgnore]
        public bool LocationKnown
        {
            get { return Latitude.HasValue && Longitude.HasValue; }
        }

        [JsonIgnore]
        public IEnumerable<string> TypeList
        {
            get
            {
                if (Types == null) return new List<string>();
                return Types.Split(new char[] {','});
            }
        }

        [JsonIgnore]
        public IList<int> RadiusOptions
        {
            get { return new int[] { 8000, 16000, 24000, 32000 }; }
        }

        [JsonIgnore]
        public string Json
        {
            get { return JsonConvert.SerializeObject(this); }
        }

        public void PopulateEntity(SearchCriteria entity, IEntityService entityService) 
        {
            foreach (var typeName in TypeList)
            {
                var type = entityService.GetEntityType(typeName);
                if (type != null) entity.EntityTypeIds.Add(type.Id);
            }

            entity.Keywords = Keywords;
            entity.StyleId = Style;
            entity.TeacherId = Teacher;
            entity.TeacherServiceId = TeacherService;
            entity.VenueId = Venue;
            entity.VenueTypeId = VenueType;
            entity.VenueServiceId = VenueService;
            entity.AccreditationBodyId = AccreditationBody;
            entity.Location = Location;
            entity.Date = Date.ToDateTime();
            entity.SortOrder = SortOrder;
            entity.Stubbed = Stubbed;
            
            if (PageIndex.HasValue) entity.PageIndex = PageIndex.Value;
            if (PageSize.HasValue) entity.PageSize = PageSize;

            if (Latitude.HasValue && Longitude.HasValue)
            {
                entity.Coordinates = new GeographicPoint(Latitude.Value, Longitude.Value);
            }

            if (NorthBound.HasValue && SouthBound.HasValue && EastBound.HasValue && WestBound.HasValue)
            {
                entity.Bounds = new SearchBounds(NorthBound.Value, SouthBound.Value, WestBound.Value, EastBound.Value);
            }
        }
    }
}