using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivitySearchCriteriaModel
    {
        public ActivitySearchCriteriaModel()
        {
            PageSize = 20;
        }

        public int? OwnerId { get; set; }

        public int? TypeId { get; set; }

        public int? EventTypeId { get; set; }

        public string Location { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? Radius { get; set; }

        public int? VenueId { get; set; }

        public int? TTOEntityId { get; set; }

        public string VenueName { get; set; }

        public int? TeacherId { get; set; }

        public string TeacherName { get; set; }

        public string Keywords { get; set; }

        public int? AbilityLevelId { get; set; }

        public int? StyleId { get; set; }

        public DateTime? StartDate { get; set; }

        public bool StartDateFlexible { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public ActivityTimeOfDay? TimeOfDay { get; set; }

        public string Duration { get; set; }

        public ActivitySortOrder SortOrder { get; set; }
        
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IList<SelectListItem> SortOrderOptions
        {
            get { return Enum.GetNames(typeof(ActivitySortOrder)).Select(n => new SelectListItem { Value = n, Text = n }).ToList(); }
        }

        public void PopulateEntity(ActivitySearchCriteria entity)
        {
            entity.OwnerId = OwnerId;
            entity.TypeId = TypeId ?? EventTypeId;
            entity.VenueId = VenueId;
            entity.VenueName = VenueName;
            entity.TeacherId = TeacherId;
            entity.TeacherName = TeacherName;
            entity.Keywords = Keywords;
            entity.AbilityLevelId = AbilityLevelId;
            entity.StyleId = StyleId;
            entity.SortOrder = SortOrder;
            entity.TTOEntityId = TTOEntityId;

            if (StartDate.HasValue)
            {
                var earliest = StartDate.Value.Date;
                var latest = earliest.AddDays(1);

                if (StartDateFlexible)
                {
                    earliest = earliest.AddDays(-10);
                    latest = earliest.AddDays(20);
                }

                entity.EarliestTime = earliest;
                entity.LatestTime = latest;
            }

            //entity.DayOfWeek = DayOfWeek;

            entity.TimeOfDay = TimeOfDay;
            entity.Duration = !string.IsNullOrEmpty(Duration) ? TimeSpan.Parse(Duration) : (TimeSpan?)null;

            if (Latitude.HasValue && Longitude.HasValue)
            {
                entity.Coordinates = new GeographicPoint(Latitude.Value, Longitude.Value);
            }

            entity.Radius = Radius;

            entity.PageIndex = PageIndex;
            entity.PageSize = PageSize;
        }

        public void PopulateStartDate()
        {
            if (DayOfWeek.HasValue)
            {
                var day = (int)DayOfWeek.Value;
                var today = (int)DateTime.Today.DayOfWeek;

                if (day > today)
                {
                    StartDate = DateTime.Today.AddDays(day - today);
                }
                else
                {
                    StartDate = DateTime.Today.AddDays(7 - (today - day));
                }
            }
        }
    }
}