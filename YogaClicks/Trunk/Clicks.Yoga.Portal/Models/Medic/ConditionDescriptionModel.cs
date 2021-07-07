using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionDescriptionModel
    {
        public ConditionDescriptionModel() {}

        public ConditionDescriptionModel(Condition condition) 
        {
            Id = condition.Id;
            Description = condition.Description;
            ImageCourtesyOf = condition.ImageCourtesyOf;
            MetaDescription = condition.MetaDescription;
            MetaTitle = condition.MetaTitle;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public string ImageCourtesyOf { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public void PopulateEntity(
            Condition condition)
        {
            condition.Description = Description;
            condition.MetaDescription = MetaDescription;
            condition.ImageCourtesyOf = ImageCourtesyOf;
            condition.MetaTitle = MetaTitle;
        }
    }
}