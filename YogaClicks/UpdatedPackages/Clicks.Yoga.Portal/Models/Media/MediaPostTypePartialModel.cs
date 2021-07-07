using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Media
{
    public class MediaPostTypePartialModel
    {
        public MediaPostTypePartialModel(IEnumerable<MediaResourceModel> resources, int? width)
        {
            Resources = resources.ToList();
            Width = width;
        }

        public IList<MediaResourceModel> Resources { get; private set; }

        public int? Width { get; private set; }
    }
}