using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityModel : PrincipalEntityModel<Activity>
    {
        public ActivityModel(Activity entity) : base(entity) {}

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool BookingRequired { get; private set; }

        public string EnquiryEmailAddress { get; private set; }

        public string Price { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime FinishTime { get; private set; }

        public VenueModel Venue { get; private set; }

        public ActivityTypeModel Type { get; private set; }

        public AbilityLevelModel AbilityLevel { get; private set; }

        public EntityHandleModel PromoterHandle { get; private set; }

        public ImageModel Image { get; private set; }

        public ImageModel ProfileBanner { get; private set; }

        public WallModel Wall { get; private set; }

        public IList<NamedSummaryModel> Teachers { get; private set; }

        public bool Finished
        {
            get { return FinishTime <= DateTime.Now; }
        }

        public bool CanMessagePromoter { get; set; }

        public bool CanBefriendPromotor { get; set; }

        public override void Populate(Activity entity)
        {
            // HACK: Naughty naughty!
            //var messaging = DependencyResolver.Current.GetService(typeof (IMessagingService)) as IMessagingService;
            //var context = DependencyResolver.Current.GetService(typeof (ISecurityContext)) as ISecurityContext;

            //CanMessagePromoter = messaging.RecipientPermitted(entity.PromoterHandle.GetEntityReference());
            //CanBefriendPromotor = entity.PromoterHandle.EntityType.GetSystemType() == typeof (Profile) && entity.PromoterHandle.EntityId != context.CurrentProfile.Id;

            Name = entity.Name;
            Description = entity.Description;
            BookingRequired = entity.BookingRequired;
            EnquiryEmailAddress = entity.Owner.EmailAddress;
            Price = entity.Price;

            var next = entity.NextRepeat(DateTime.Today.AddDays(1));

            if (next == null)
            {
                StartTime = entity.StartTime;
                FinishTime = entity.FinishTime;
            }
            else
            {
                StartTime = next.StartTime;
                FinishTime = next.FinishTime;
            }

            Venue = new VenueModel(entity.Venue);
            Type = new ActivityTypeModel(entity.Type);
            AbilityLevel = new AbilityLevelModel(entity.AbilityLevel);
            PromoterHandle = new EntityHandleModel(entity.PromoterHandle);
            Image = new ImageModel(entity.Image);
            ProfileBanner = new ImageModel(entity.ProfileBanner);
            Wall = new WallModel(entity.Wall);

            Teachers = new List<NamedSummaryModel>();

            foreach (var association in entity.Teachers.Where(t => t.Teacher.Active))
            {
                if (!association.Confirmed) continue;
                Teachers.Add(new NamedSummaryModel(association.Teacher, association.Teacher.Stubbed));
            }
        }
    }
}