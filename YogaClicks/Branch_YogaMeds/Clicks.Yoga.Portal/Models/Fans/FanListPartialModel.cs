using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Fans
{
    public class FanListPartialModel
    {
        public FanListPartialModel(EntityHandle handle, IEnumerable<Profile> fans)
        {
            Handle = new EntityHandleModel(handle);
            Fans = new List<ProfileSummaryModel>();

            foreach (var fan in fans)
            {
                Fans.Add(new ProfileSummaryModel(fan));
            }
        }

        public EntityHandleModel Handle { get; private set; }

        public IList<ProfileSummaryModel> Fans { get; private set; }
    }
}