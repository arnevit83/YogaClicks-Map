using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateClassStep3Model
    {
        public ActivityCreateClassStep3Model()
        {
            Styles = new StyleChooserModel();
        }

        [DataMember]
        public bool BookingRequired { get; set; }

        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public StyleChooserModel Styles { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(Activity activity, IStyleService styleService)
        {
            activity.BookingRequired = BookingRequired;
            activity.Price = Price ?? "";
            activity.Description = Description ?? "";

            Styles.PopulateCollection(activity.Styles, styleService);
        }

        public ActivityCreateClassStep3Model PopulateMetadata(IStyleService styleService)
        {
            Styles.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}