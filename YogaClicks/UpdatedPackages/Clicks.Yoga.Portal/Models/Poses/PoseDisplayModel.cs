using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class PoseDisplayModel
    {
        public PoseDisplayModel(Pose style, IEnumerable<PoseCategory> categories, IEnumerable<Pose> poses, IEnumerable<PoseUserImages> PPImages, IEnumerable<Condition> condition)
        {
            Pose = new PoseDetailModel(style);
            Categories = new List<PoseCategoryModel>();
            Poses = new List<PoseModel>();
            ImagesOfPoses = new List<PoseUserImages>();
            Conditions = new List<Condition>();

            MainVideo = style.MainVideo;


            foreach (var d in condition)
            {
                Conditions.Add(d);
            }
       
            foreach (var c in categories)
            {
                Categories.Add(new PoseCategoryModel(c));
            }

            foreach (var p in poses)
            {
                Poses.Add(new PoseModel(p));
            }
            foreach (var i in PPImages)
            {
                ImagesOfPoses.Add(i);
            }
        }

        public PoseDetailModel Pose { get; private set; }

        public String MainVideo { get; private set; }

        public IList<PoseCategoryModel> Categories { get; private set; }

        public IList<PoseModel> Poses { get; private set; }
        public IList<PoseUserImages> ImagesOfPoses { get; private set; }
        public IList<Condition> Conditions { get; private set; }
    }
}