using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionUpdateModel
    {
        public ConditionUpdateModel() { }

        public ConditionUpdateModel(Condition condition)
        {
            Id = condition.Id;
            Name = condition.Name;
            Description = condition.Description;
            MetaDescription = condition.MetaDescription;
            ProfileBanner = new ConditionBannerEditorModel(condition.ProfileBanner);
            DirectoryImage = new ConditionDirectoryImageEditorModel(condition.DirectoryImage);
            ImageCourtesyOf = condition.ImageCourtesyOf;
            UserStories = new UserStoriesModel(condition.UserStories);
            Studies = new StudiesModel(condition.Studies);
            WhatTheScienceSayses = new WhatTheScienceSaysesModel(condition.WhatTheScienceSayses);
            MetaTitle = condition.MetaTitle;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public ConditionBannerEditorModel ProfileBanner { get; set; }

        public ConditionDirectoryImageEditorModel DirectoryImage { get; set; }

        public string ImageCourtesyOf { get; set; }

        public UserStoriesModel UserStories { get; set; }

        public WhatTheScienceSaysesModel WhatTheScienceSayses { get; set; }

        public StudiesModel Studies { get; set; }


    }
}