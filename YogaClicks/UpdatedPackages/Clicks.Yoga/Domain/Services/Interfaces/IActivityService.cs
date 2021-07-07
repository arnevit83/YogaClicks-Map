using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IActivityService
    {
        IList<Activity> LuckyDip();
        IList<AbilityLevel> GetAbilityLevels();
        IList<ActivityType> GetActivityTypes();
        IList<ActivityRepeatFrequency> GetRepeatFrequencies();
        IList<ActivityTeacher> GetTeachers(int activityId);
        IList<ActivityAttendee> GetAttendees(int activityId);
        IList<Profile> GetInvitableFriends(int activityId);
        ActivitySearchResponse SearchActivities(ActivitySearchCriteria criteria);
        IList<ActivityRepeatInstance> SearchClasses(ActivitySearchCriteria criteria);
        IList<ActivityAttendee> GetUpcomingAttendances(EntityReference actor);
        AbilityLevel GetAbilityLevel(int id);
        ActivityType GetActivityType(int id);
        ActivityRepeatFrequency GetRepeatFrequency(int id);
        ActivityRepeatFrequency GetSingleRepeatFrequency();
        Activity GetActivity(int id);
        List<Activity> GetActivitiesAndTeachersForPromoter(int promoterId);
        List<Activity> GetEventsForYear(int promoterId, bool isTeacher, bool isVenue, bool isTto);
        Activity GetActivityForDisplay(int id);
        Activity GetActivityForEditing(int id);
        ActivityAttendanceStatus GetAttendanceStatus(int activityId, IActor actor);
        ActivityPermissions GetPermissions(int activityId);
        void CreateActivity(Activity activity);
        void AddAttendee(int activityId, IActor actor);
        void AddAttendee(Activity activity, IActor actor);
        void RemoveAttendee(int activityId, IActor actor);
        void RemoveAttendee(Activity activity, IActor actor);
        void AddActivityTeacher(int activityId, int teacherId);
        void AddActivityTeacher(Activity activity, Teacher teacher);
        void RemoveActivityTeacher(int activityId, int teacherId);
        void RemoveActivityTeacher(Activity activity, Teacher teacher);
        void AddActivityVenue(int activityId, int venueId);
        void AddActivityVenue(Activity activity, Venue venue);
        //void RemoveActivityVenue(Activity activity, Venue venue);
        void SetActivityVenue(int activityId, int venueId);
        void SetActivityVenue(Activity activity, Venue venue);
        void InviteFriends(int activityId, IEnumerable<int> friendIds);
        void InviteFans(int activityId);
        void SetClassServices(Activity activity);
    }
}