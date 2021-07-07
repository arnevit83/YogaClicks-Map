using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivitySettingsModel
    {
        public ActivitySettingsModel() {}

        public ActivitySettingsModel(Activity activity)
        {
            Activity = new ActivityModel(activity);
            Public = activity.Public;
            AttendeeInvitationsPermitted = activity.AttendeeInvitationsPermitted;
            AttendeePostingPermitted = activity.AttendeePostingPermitted;
            AttendeeGalleriesPermitted = activity.AttendeeGalleriesPermitted;
        }

        public ActivityModel Activity { get; private set; }

        public bool Public { get; set; }

        public bool AttendeeInvitationsPermitted { get; set; }

        public bool AttendeePostingPermitted { get; set; }

        public bool AttendeeGalleriesPermitted { get; set; }

        public void PopulateEntity(Activity activity)
        {
            activity.Public = Public;
            activity.AttendeeInvitationsPermitted = AttendeeInvitationsPermitted;
            activity.AttendeePostingPermitted = AttendeePostingPermitted;
            activity.AttendeeGalleriesPermitted = AttendeeGalleriesPermitted;
        }

        public ActivitySettingsModel PopuplateMetadata(Activity activity)
        {
            Activity = new ActivityModel(activity);
            return this;
        }
    }
}