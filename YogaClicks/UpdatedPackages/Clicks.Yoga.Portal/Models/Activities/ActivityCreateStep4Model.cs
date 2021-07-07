using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateStep4Model
    {
        [DataMember]
        public int VenueId { get; set; }

        public bool Back { get; set; }

        public void PopulateEntity(Activity activity, IActivityService activityService, IVenueService venueService)
        {
            activityService.AddActivityVenue(activity, venueService.GetVenue(VenueId));
        }
    }
}