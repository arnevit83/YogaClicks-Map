using System.Collections.Generic;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class PoseIndexModel
    {
        public PoseIndexModel(IEnumerable<PoseCategory> categories, IEnumerable<Pose> poses, int categoryId) : this(categories, poses)
        {
            CategoryId = categoryId;
        }

        public PoseIndexModel(IEnumerable<PoseCategory> categories, IEnumerable<Pose> poses, Pose pose) : this(categories, poses)
        {
            if (pose != null) CategoryId = pose.Category.Id;
        }

        public PoseIndexModel(IEnumerable<PoseCategory> categories, IEnumerable<Pose> poses)
        {
            Poses = new List<PoseModel>();
            Categories = new List<PoseCategoryModel>();

            foreach (var pose in poses)
                Poses.Add(new PoseModel(pose));

            foreach (var category in categories)
            {
                var cat = new PoseCategoryModel(category);

                Categories.Add(cat);

                TotalCount += cat.Count;
            }
        }

        public int? CategoryId { get; set; }

        public int TotalCount { get; set; }

        public IList<PoseModel> Poses { get; private set; }

        public IList<PoseCategoryModel> Categories { get; private set; }

        public IEnumerable<SelectListItem> AbilityLevelOptions 
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "1", Text = "1"},
                    new SelectListItem { Value = "2", Text = "2"},
                    new SelectListItem { Value = "3", Text = "3"}
                };
            }
        }
    }
}
