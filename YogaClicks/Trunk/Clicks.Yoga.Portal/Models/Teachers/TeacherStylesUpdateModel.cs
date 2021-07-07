using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherStylesUpdateModel
    {
         public TeacherStylesUpdateModel()
        {
            Styles = new StyleChooserModel();
        }

         public TeacherStylesUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            Styles = new StyleChooserModel(teacher.Styles);
        }

        public void PopulateEntity(
            Teacher entity,
            IStyleService styleService)
        {
            Styles.PopulateCollection(entity.Styles, styleService);
        }

        public int TeacherId { get; set; }

        public StyleChooserModel Styles { get; set; }

        public TeacherStylesUpdateModel PopulateMetadata(
            Teacher teacher,
            IStyleService styleService)
        {
            Styles.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}