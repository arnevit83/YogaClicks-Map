using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherImageUpdateModel
    {
          public TeacherImageUpdateModel()
        {
        }

          public TeacherImageUpdateModel(Teacher teacher)
        {
            Image = new ProfileImageEditorModel(teacher.Image);
            TeacherId = teacher.Id;
        }
          public ProfileImageEditorModel Image { get; set; }

        public int TeacherId { get; set; }

        public void PopulateEntity(
            Teacher entity,
            IImageService imageService)
        {
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
        }
    }
}