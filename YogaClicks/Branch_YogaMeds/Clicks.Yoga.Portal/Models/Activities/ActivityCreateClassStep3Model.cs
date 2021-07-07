using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Domain.Services.Interfaces;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateClassStep3Model
    {
        public ActivityCreateClassStep3Model()
        {
            Styles = new StyleChooserModel();
            Conditions = new ConditionChooserModel();
        }

        [DataMember]
        public bool BookingRequired { get; set; }

        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public StyleChooserModel Styles { get; private set; }

        [DataMember]
        public ConditionChooserModel Conditions { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(Activity activity, IStyleService styleService, IMedicService medicService)
        {
            activity.BookingRequired = BookingRequired;
            activity.Price = Price ?? "";
            activity.Description = Description ?? "";

            Styles.PopulateCollection(activity.Styles, styleService);
            Conditions.PopulateCollection(activity.Conditions, medicService);
        }

        public ActivityCreateClassStep3Model PopulateMetadata(IStyleService styleService, IMedicService medicService)
        {
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
            return this;
        }
    }
}