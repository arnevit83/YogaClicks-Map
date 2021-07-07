using System;
using System.Collections.Generic;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileNotificationsPeriodModel
    {
        public ProfileNotificationsPeriodModel(DateTime date)
        {
            Deliveries = new List<NotificationDeliveryModel>();
            Date = date;
        }

        public DateTime Date { get; set; }

        public string Name
        {
            get
            {
                return Date == DateTime.Now.Date ? "Today" : Date.ToString("dd MMMM");
            }
        }

        public IList<NotificationDeliveryModel> Deliveries { get; private set; }
    }
}