using System;
using System.Collections.Generic;
using System.Data.Spatial;
using Clicks.Yoga.Permissions.Providers;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class Activity : PrincipalEntity, IEntityHandle, IGalleryAssociate, INamed, ISearchable, IProfileBanner
    {
        public Activity()
        {
            Attendees = new List<ActivityAttendee>();
            Teachers = new List<ActivityTeacher>();
            Styles = new List<Style>();
            Conditions = new List<Condition>();
        }

        public string Name { get; set; }

        public virtual ActivityType Type { get; set; }
        
        public virtual EntityHandle PromoterHandle { get; set; }

        public virtual AbilityLevel AbilityLevel { get; set; }
        
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public virtual ActivityRepeatFrequency RepeatFrequency { get; set; }

        public bool BookingRequired { get; set; }

        public string Price { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public virtual ActivityWall Wall { get; set; }

        public virtual ICollection<ActivityAttendee> Attendees { get; private set; }

        public virtual ICollection<ActivityTeacher> Teachers { get; private set; }

        public virtual ActivityVenue ActivityVenue { get; set; }
        public virtual Venue Venue { get; set; }

        public virtual ICollection<Style> Styles { get; private set; }
        
        public virtual ICollection<Condition> Conditions { get; private set; }

        public bool Public { get; set; }

        public bool AttendeeInvitationsPermitted { get; set; }

        public bool AttendeePostingPermitted { get; set; }

        public bool AttendeeGalleriesPermitted { get; set; }

        public ActivityRepeatInstance NextRepeat(DateTime date)
        {
            if (date.Date != date)
                throw new ArgumentException("Date must not have a time component.", "date");

            if (date <= StartTime) return new ActivityRepeatInstance(this, StartTime, FinishTime);

            if (RepeatFrequency == null) return null;

            var start = RepeatFrequency.NextRepeatTime(StartTime, date);

            if (start == null) return null;

            var end = new DateTime(start.Value.Year, start.Value.Month, start.Value.Day, FinishTime.Hour, FinishTime.Minute, 0);

            return new ActivityRepeatInstance(this, start.Value, end);
        }

        public bool IsSearchable
        {
            get { return Active && Public && Type != null; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name ?? "";
            record.Description = Description ?? "";
            record.Image = Image;

            if (ActivityVenue.Venue.Address != null)
            {
                record.City = ActivityVenue.Venue.Address.City;
                record.Area = ActivityVenue.Venue.Address.Area;
                record.Country = ActivityVenue.Venue.Address.Country != null ? ActivityVenue.Venue.Address.Country.EnglishName : null;
                record.Postcode = ActivityVenue.Venue.Address.Postcode;
                record.Location = ActivityVenue.Venue.Address.Location;
            }

            record.UpdateEntities(new List<IEnumerable<INamed>> { Styles, Teachers.Select(t => t.Teacher) });

        }

        public IEnumerable<ISearchable> GetChildSearchables()
        {
            yield break;
        }

        public Type GetGalleryAssociatePermissionProviderType()
        {
            return typeof(ActivityGalleryPermissionProvider);
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

        public string ProductDescription { get; set; }
        public string ProductButton { get; set; }
    }
}