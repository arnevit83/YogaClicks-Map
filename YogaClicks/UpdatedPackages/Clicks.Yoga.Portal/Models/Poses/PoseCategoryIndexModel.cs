using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class PoseCategoryIndexModel
    {
        public PoseCategoryIndexModel(IEnumerable<PoseCategory> categories)
        {
            Categories = new List<PoseCategoryModel>();

            foreach (var category in categories)
            {
                Categories.Add(new PoseCategoryModel(category));
            }
        }

        public IList<PoseCategoryModel> Categories { get; private set; }
    }
}