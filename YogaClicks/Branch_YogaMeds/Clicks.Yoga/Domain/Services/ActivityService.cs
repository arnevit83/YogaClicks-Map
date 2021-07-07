using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Services
{
    public class ActivityService : ServiceBase, IActivityService
    {
        public ActivityService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService,
            INewsService newsService,
            IProfileService profileService,
            IRequestService requestService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            FriendService = friendService;
            NewsService = newsService;
            ProfileService = profileService;
            RequestService = requestService;
        }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private INewsService NewsService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IRequestService RequestService { get; set; }

        public IList<Activity> LuckyDip()
        {
            return EntityContext.Activities.Where(e => e.StartTime > DateTime.Now).OrderBy(e => Guid.NewGuid()).Take(5).ToList();
        }

        public IList<AbilityLevel> GetAbilityLevels()
        {
            return EntityContext.AbilityLevels.OrderBy(l => l.Index).ToList();
        }

        public IList<ActivityType> GetActivityTypes()
        {
            return EntityContext.ActivityTypes.OrderBy(x=>x.Name).ToList();
        }

        public IList<ActivityRepeatFrequency> GetRepeatFrequencies()
        {
            return EntityContext.ActivityRepeatFrequencies.ToList();
        }

        public IList<ActivityTeacher> GetTeachers(int activityId)
        {
            return EntityContext.ActivityTeachers
                .Include(e => e.Teacher)
                .Include(e => e.Teacher.Image)
                .Where(e => e.Activity.Id == activityId)
                .Where(e => e.Teacher.Active)
                .ToList();
        }

        public IList<ActivityAttendee> GetAttendees(int activityId)
        {
            return EntityContext.ActivityAttendees
                .Include(e => e.Handle)
                .Include(e => e.Handle.EntityType)
                .Include(e => e.Handle.Image)
                .Where(e => e.Activity.Id == activityId)
                .Where(e => e.Handle.Active)
                .ToList();
        }

        public IList<Profile> GetInvitableFriends(int activityId)
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var permissions = GetPermissions(activityId);

            if (!permissions.Invite) return new List<Profile>();

            var attendees = EntityContext.ActivityAttendees.Where(a => a.Activity.Id == activityId);

            return EntityContext.Friendships
                .Where(f => f.Profile.Owner.Id == SecurityContext.CurrentUser.Id)
                .Where(f => f.Friend.Active)
                .Where(f => f.Confirmed)
                .Where(f => !attendees.Any(a =>
                    a.Handle.Owner.Id == f.Friend.Owner.Id &&
                    (!a.Confirmed.HasValue || a.Confirmed.Value)))
                .Select(f => f.Friend)
                .ToList();
        }

        public ActivitySearchResponse SearchActivities(ActivitySearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var query = DefaultSearchQuery(criteria);

            query = query.Where(a => !a.Type.IsClass);

            if (criteria.TypeId != null)
            {
                query = query.Where(a => a.Type.Id == criteria.TypeId);
            }

            if (criteria.Promoter.HasValue)
            {
                var entityId = criteria.Promoter.Value.EntityId;
                var entityTypeName = criteria.Promoter.Value.EntityTypeName;

                query = query.Where(a =>
                    a.PromoterHandle.EntityId == entityId &&
                    a.PromoterHandle.EntityType.Name == entityTypeName);
            }

            if (criteria.Attendee.HasValue)
            {
                var entityId = criteria.Promoter.Value.EntityId;
                var entityTypeName = criteria.Promoter.Value.EntityTypeName;

                query = query.Where(a => a.Attendees.Any(p =>
                    p.Handle.EntityId == entityId &&
                    p.Handle.EntityType.Name == entityTypeName));
            }

            if (criteria.EarliestTime.HasValue)
            {
                query = query.Where(a => a.StartTime >= criteria.EarliestTime.Value);
            }

            if (criteria.LatestTime.HasValue)
            {
                query = query.Where(a => a.StartTime <= criteria.LatestTime.Value);
            }

            if (!criteria.EarliestTime.HasValue && !criteria.LatestTime.HasValue)
            {
                query = query.Where(a => a.StartTime > DateTime.Today);
            }

            if (criteria.PageSize.HasValue)
            {
                query = query.Skip(criteria.PageIndex * criteria.PageSize.Value).Take(criteria.PageSize.Value + 1);
            }

            var results = query.ToList();
            var hasNextPage = criteria.PageSize.HasValue && results.Count > criteria.PageSize;
            var hasPreviousPage = criteria.PageIndex > 0;

            if (hasNextPage)
            {
                results.Remove(results.Last());
            }

            return new ActivitySearchResponse(results, hasNextPage, hasPreviousPage);
        }

        public IList<ActivityRepeatInstance> SearchClasses(ActivitySearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");
            if (!criteria.EarliestTime.HasValue || !criteria.LatestTime.HasValue)
                throw new ArgumentException("Class searches must specify a time range.", "criteria");
            if (criteria.EarliestTime >= criteria.LatestTime)
                throw new ArgumentException("Earliest time must be greater than latest time.");

            var query = DefaultSearchQuery(criteria);

            query = query.Where(a => a.Type.IsClass);

            query = query.Where(a => a.RepeatFrequency.IsSingle ? a.StartTime >= criteria.EarliestTime : a.StartTime < criteria.LatestTime);
            query = query.Where(a => !a.RepeatFrequency.IsSingle || a.FinishTime <= criteria.LatestTime);

            if (criteria.DayOfWeek != null)
            {
                var dayNumber = ((int)criteria.DayOfWeek) + 1;
                query = query.Where(a => SqlFunctions.DatePart("weekday", a.StartTime) == dayNumber);
            }

            if (criteria.TimeOfDay != null)
            {
                TimeSpan startTime = new TimeSpan(0, 0, 0);
                TimeSpan? endTime = null;

                switch (criteria.TimeOfDay.Value)
                {
                    case ActivityTimeOfDay.Morning:
                        endTime = new TimeSpan(12, 0, 0);
                        break;
                    case ActivityTimeOfDay.Afternoon:
                        startTime = new TimeSpan(12, 0, 0);
                        endTime = new TimeSpan(18, 0, 0);
                        break;
                    case ActivityTimeOfDay.Evening:
                        startTime = new TimeSpan(18, 0, 0);
                        break;
                }

                query = query.Where(a => a.StartTime.Hour >= startTime.Hours);

                if (endTime != null) query = query.Where(a => a.StartTime.Hour <= endTime.Value.Hours);
            }

            if (criteria.Duration.HasValue)
            {
                var durationHours = criteria.Duration.Value.TotalHours;
                query = query.Where(a => SqlFunctions.DateDiff("hour", a.StartTime, a.FinishTime) == durationHours);
            }

            var activities = query.ToList();
            var repeats = new List<ActivityRepeatInstance>();

            foreach (var activity in activities)
            {
                var date = criteria.EarliestTime.Value.Date;
                var repeat = activity.NextRepeat(date);

                while (repeat != null && repeat.StartTime < criteria.LatestTime)
                {
                    repeats.Add(repeat);
                    date = repeat.StartTime.Date.AddDays(1);
                    repeat = activity.NextRepeat(date);
                }
            }

            return repeats.OrderBy(r => r.StartTime).ToList();
        }

        public IList<ActivityAttendee> GetUpcomingAttendances(EntityReference actor)
        {
            return EntityContext.ActivityAttendees
                .Include(e => e.Activity.Type)
                .Include(e => e.Activity.Venue)
                .Where(e => e.Handle.EntityId == actor.EntityId)
                .Where(e => e.Handle.EntityType.Name == actor.EntityTypeName)
                .Where(e => e.Handle.Active)
                .Where(e => e.Activity.Active)
                .Where(e => e.Activity.StartTime > DateTime.Now)
                .OrderBy(e => e.Activity.StartTime)
                .ToList();
        }

        public AbilityLevel GetAbilityLevel(int id)
        {
            return EntityContext.AbilityLevels.Get(id);
        }

        public ActivityType GetActivityType(int id)
        {
            return EntityContext.ActivityTypes.Get(id);
        }

        public ActivityRepeatFrequency GetRepeatFrequency(int id)
        {
            return EntityContext.ActivityRepeatFrequencies.Get(id);
        }

        public ActivityRepeatFrequency GetSingleRepeatFrequency()
        {
            return EntityContext.ActivityRepeatFrequencies.Get(e => e.IsSingle);
        }

        public Activity GetActivity(int id)
        {
            return EntityContext.Activities.Get(id);
        }

        public List<Activity> GetActivitiesAndTeachersForPromoter(int promoterId)
        {
            return EntityContext.Activities
                   .Include(x => x.Teachers)
                   .Include(x => x.RepeatFrequency)
                   .Where(x => x.PromoterHandle.EntityId == promoterId)
                   .ToList();
        }

        public List<Activity> GetEventsForYear(int promoterId)
        {
            return EntityContext.Activities
                   .Where(x => x.PromoterHandle.EntityId == promoterId)
                   .Where(x => !x.Type.IsClass)
                   .Where(x => x.StartTime > DateTime.Now && x.StartTime.Year == DateTime.Now.Year)
                   .Where(x => x.Active == true)
                   .OrderBy(x => x.StartTime)
                   .ToList();
        }

        public Activity GetActivityForDisplay(int id)
        {
            var activity = GetActivity(id);
            if (activity == null) throw new UnknownEntityException();
            return activity;
        }

        public Activity GetActivityForEditing(int id)
        {
            var activity = GetActivity(id);
            if (!SecurityContext.CanUpdate(activity)) throw new PermissionDeniedException();
            return activity;
        }

        public ActivityAttendanceStatus GetAttendanceStatus(int activityId, IActor actor)
        {
            var activity = GetActivity(activityId);

            var attendance = EntityContext.ActivityAttendees.FirstOrDefault(e =>
                e.Activity.Id == activityId &&
                e.Handle.Owner.Id == actor.Owner.Id);

            if (attendance != null)
            {
                return attendance.GetAttendanceStatus();
            }

            return activity.Public ? ActivityAttendanceStatus.Open : ActivityAttendanceStatus.Closed;
        }

        public ActivityPermissions GetPermissions(int activityId)
        {
            var authenticated = SecurityContext.Authenticated;
            var user = authenticated ? SecurityContext.CurrentUser : null;
            var activity = GetActivity(activityId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");

            var attendee = authenticated ? EntityContext.ActivityAttendees.FirstOrDefault(e =>
                    e.Activity.Id == activityId &&
                    e.Handle.Owner.Id == user.Id &&
                    e.Confirmed.HasValue &&
                    e.Confirmed.Value)
                    : null;
            var updatable = SecurityContext.CanUpdate(activity);
            var attending = attendee != null;

            return new ActivityPermissions
            {
                Access = updatable || attending || activity.Public,
                Invite = updatable || (attending && activity.AttendeeInvitationsPermitted),
                Manage = updatable
            };
        }

        public void CreateActivity(Activity activity)
        {
            if (activity.Owner == null)
                throw new ArgumentException("Activity must have an owner.", "activity");
            if (!SecurityContext.CanUpdate(activity.Owner))
                throw new PermissionDeniedException();
            if (activity.PromoterHandle == null)
                throw new ArgumentException("Activity must have a promoter.", "activity");

            activity.Wall = new ActivityWall();
            activity.AttendeeInvitationsPermitted = true;
            activity.AttendeePostingPermitted = true;
            activity.AttendeeGalleriesPermitted = true;

            EntityContext.Activities.Add(activity);

            var promoter = activity.PromoterHandle;
            var profile = activity.Owner.Profile;

            if (promoter.EntityId == profile.Id && promoter.EntityType.Name == profile.GetEntityTypeName())
            {
                AddAttendee(activity, profile);
            }

            NewsService.PostAction(NewsStoryType.FriendAddedEntity, activity.PromoterHandle, activity);
        }

        public void AddAttendee(int activityId, IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");
            if (!SecurityContext.CanUpdate(actor))
                throw new PermissionDeniedException();

            var activity = GetActivity(activityId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");

            AddAttendee(activity, actor);
        }

        public void AddAttendee(Activity activity, IActor actor)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");
            if (actor == null)
                throw new ArgumentNullException("actor");
            if (!SecurityContext.CanUpdate(actor))
                throw new PermissionDeniedException();

            var handle = EntityService.EnsureEntityHandle(actor);

            if (!handle.EntityType.IsHuman)
                throw new PermissionDeniedException();

            var attendee = EntityContext.ActivityAttendees.Get(a =>
                a.Activity.Id == activity.Id &&
                a.Handle.Owner.Id == actor.Owner.Id);

            if (attendee == null)
            {
                attendee = new ActivityAttendee
                {
                    Activity = activity,
                    Handle = handle,
                    Confirmed = true
                };

                EntityContext.ActivityAttendees.Add(attendee);
            }
            else
            {
                attendee.Handle = handle;
                attendee.Confirmed = true;
            }

            NewsService.PostAction(NewsStoryType.FriendAttendingActivity, attendee.Handle, attendee.Activity);
        }

        public void RemoveAttendee(int activityId, IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");
            if (!SecurityContext.CanUpdate(actor))
                throw new PermissionDeniedException();

            var activity = GetActivity(activityId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");

            RemoveAttendee(activity, actor);
        }

        public void RemoveAttendee(Activity activity, IActor actor)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");
            if (actor == null)
                throw new ArgumentNullException("actor");
            if (!SecurityContext.CanUpdate(actor))
                throw new PermissionDeniedException();

            var handle = EntityService.EnsureEntityHandle(actor);

            var attendee = EntityContext.ActivityAttendees.Get(a =>
                a.Activity.Id == activity.Id &&
                a.Handle.Owner.Id == actor.Owner.Id);

            if (attendee == null)
            {
                attendee = new ActivityAttendee
                {
                    Activity = activity,
                    Handle = handle,
                    Confirmed = false
                };

                EntityContext.ActivityAttendees.Add(attendee);
            }
            else
            {
                attendee.Handle = handle;
                attendee.Confirmed = false;
            }
        }

        public void AddActivityTeacher(int activityId, int teacherId)
        {
            var activity = GetActivity(activityId);
            var teacher = EntityContext.Teachers.Get(teacherId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");
            if (teacher == null)
                throw new ArgumentOutOfRangeException("teacherId");

            AddActivityTeacher(activity, teacher);
        }

        public void AddActivityTeacher(Activity activity, Teacher teacher)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");
            if (teacher == null)
                throw new ArgumentNullException("teacher");
            if (!SecurityContext.CanUpdate(activity))
                throw new PermissionDeniedException();

            if (activity.Teachers.Any(e => e.Teacher.Id == teacher.Id)) return;

            var confirmed = teacher.Owner == null || teacher.Owner.Id == activity.Owner.Id;

            var association = new ActivityTeacher
            {
                Activity = activity,
                Teacher = teacher,
                Confirmed = confirmed
            };

            activity.Teachers.Add(association);

            if (!association.Confirmed && teacher.Owner != null)
            {
                RequestService.Request("ActivityTeacherAdded", teacher.Owner, SecurityContext.CurrentProfile, activity, teacher, association);
            }

        }

        public void RemoveActivityTeacher(int activityId, int teacherId)
        {
            var activity = GetActivity(activityId);
            var teacher = EntityContext.ActivityTeachers.Where(e => e.Teacher.Id == teacherId).Select(e => e.Teacher).FirstOrDefault();

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");
            if (teacher == null)
                throw new ArgumentOutOfRangeException("teacherId");

            RemoveActivityTeacher(activity, teacher);
        }

        public void RemoveActivityTeacher(Activity activity, Teacher teacher)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");
            if (teacher == null)
                throw new ArgumentNullException("teacher");
            if (!SecurityContext.CanUpdateAny(activity, teacher))
                throw new PermissionDeniedException();

            var association = activity.Teachers.FirstOrDefault(e => e.Teacher.Id == teacher.Id);

            if (association == null) return;

            EntityContext.ActivityTeachers.Remove(association);
            activity.Teachers.Remove(association);
        }

        public void SetActivityVenue(int activityId, int venueId)
        {
            var activity = GetActivity(activityId);
            var venue = EntityContext.Venues.Get(venueId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");
            if (venue == null)
                throw new ArgumentOutOfRangeException("venueId");

            SetActivityVenue(activity, venue);
        }

        public void SetActivityVenue(Activity activity, Venue venue)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");
            if (venue == null)
                throw new ArgumentNullException("venue");
            if (!SecurityContext.CanUpdate(activity))
                throw new PermissionDeniedException();

            if ((activity.Venue == null || activity.Venue.Id != venue.Id) && !SecurityContext.CanUpdate(venue) && venue.Owner != null)
            {
                RequestService.Request("ActivityVenueAdded", venue.Owner, SecurityContext.CurrentProfile, activity, venue, activity);
            }

            activity.Venue = venue;
        }

        public void InviteFriends(int activityId, IEnumerable<int> friendIds)
        {
            var activity = GetActivity(activityId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");

            var permissions = GetPermissions(activityId);

            if (!permissions.Invite)
                throw new PermissionDeniedException();

            var friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);

            foreach (var friendId in friendIds)
            {
                var friend = friends.FirstOrDefault(e => e.Id == friendId);

                if (friend == null) continue;

                InviteProfile(friend, activity);
            }
        }

        public void InviteFans(int activityId)
        {
            var activity = GetActivity(activityId);

            if (activity == null)
                throw new ArgumentOutOfRangeException("activityId");

            var permissions = GetPermissions(activityId);

            if (!permissions.Invite)
                throw new PermissionDeniedException();

            foreach (var fan in ProfileService.GetFans())
            {
                InviteProfile(fan, activity);
            }
        }

        public void SetClassServices(Activity activity)
        {
            if (activity == null || !activity.Type.IsClass)
                return;

            var service = EntityContext.TeacherServices.Get(e => e.IsClass);

            if (service == null)
                return;

            foreach (var teacher in activity.Teachers.Where(e => !e.Teacher.Services.Any(s => s.IsClass)))
            {
                teacher.Teacher.Services.Add(service);
            }
        }

        private void InviteProfile(Profile profile, Activity activity)
        {
            var handle = EntityService.EnsureEntityHandle(profile);
            var attendee = EntityContext.ActivityAttendees.Get(a =>
                a.Activity.Id == activity.Id &&
                a.Handle.EntityId == handle.EntityId &&
                a.Handle.EntityType.Name == handle.EntityType.Name);

            if (attendee != null) return;

            attendee = new ActivityAttendee
            {
                Activity = activity,
                Handle = handle
            };

            EntityContext.ActivityAttendees.Add(attendee);

            RequestService.Request("ActivityInvitation", profile.Owner, SecurityContext.CurrentActor, activity, null, attendee);
        }

        private IQueryable<Activity> DefaultSearchQuery(ActivitySearchCriteria criteria)
        {
            var query = EntityContext.Activities
                .Include(a => a.PromoterHandle)
                .Include(a => a.PromoterHandle.EntityType)
                .Include(a => a.Venue)
                .Include(a => a.Teachers)
                .Where(e => e.Public)
                .Where(e => e.Active);

            if (criteria.OwnerId.HasValue)
            {
                query = query.Where(a => a.Owner.Id == criteria.OwnerId.Value);
            }

            if (criteria.Coordinates != null && criteria.Radius.HasValue && criteria.Radius.Value > 0)
            {
                var geography = DbGeographyFunctions.PointToGeography(criteria.Coordinates);
                query = query.Where(a => a.Venue.Address.LocationGeography.Distance(geography) <= criteria.Radius);
            }

            if (criteria.VenueId.HasValue)
            {
                query = query.Where(a => a.Venue.Id == criteria.VenueId);
            }

            if (criteria.TTOEntityId.HasValue)
            {
                query = query.Where(a => a.PromoterHandle.Id == criteria.TTOEntityId);
            }

            if (!string.IsNullOrEmpty(criteria.VenueName))
            {
                var venueParts = Regex.Split(criteria.VenueName, "\\s+");
                query = query.Where(a => venueParts.Any(s => a.Venue.Name.Contains(s)));
            }

            if (criteria.TeacherId.HasValue)
            {
                query = query.Where(a => a.Teachers.Any(t => t.Teacher.Id == criteria.TeacherId));
            }

            if (!string.IsNullOrEmpty(criteria.TeacherName))
            {
                var teacherParts = Regex.Split(criteria.TeacherName, "\\s+");
                query = query.Where(a => teacherParts.Any(s => a.Teachers.Any(p => p.Teacher.Name.Contains(s))));
            }

            if (!string.IsNullOrEmpty(criteria.Keywords))
            {
                var keywordParts = Regex.Split(criteria.Keywords, "\\s+");
                query = query.Where(a => keywordParts.Any(s => a.Name.Contains(s)));
            }

            if (criteria.AbilityLevelId.HasValue)
            {
                var level = GetAbilityLevel(criteria.AbilityLevelId.Value);

                if (level.IsOpen)
                {
                    query = query.Where(a => a.AbilityLevel.IsOpen);
                }
                else
                {
                    query = query.Where(a => a.AbilityLevel.IsOpen || a.AbilityLevel.Id == criteria.AbilityLevelId.Value);
                }
            }

            if (criteria.StyleId.HasValue)
            {
                query = query.Where(a => a.Styles.Any(s => s.Id == criteria.StyleId.Value));
            }

            if (criteria.ConditionId.HasValue)
            {
                query = query.Where(a => a.Conditions.Any(s => s.Id == criteria.ConditionId.Value));
            }

            switch (criteria.SortOrder)
            {
                case ActivitySortOrder.Date:
                    query = query.OrderBy(a => a.StartTime);
                    break;
                case ActivitySortOrder.Distance:
                    if (criteria.Coordinates != null)
                    {
                        var geography = DbGeographyFunctions.PointToGeography(criteria.Coordinates);
                        query = query.OrderBy(a => a.Venue.Address.LocationGeography.Distance(geography));
                    }
                    else
                    {
                        query = query.OrderBy(a => a.StartTime);
                    }

                    break;
            }

            return query;
        }
    }
}