using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherBannerUpdateModel
    {
        public TeacherBannerUpdateModel()
        {
            ProfileBanner = new ProfileBannerEditorModel();
        }

        public TeacherBannerUpdateModel(Teacher teacher)
        {
            ProfileBanner = new ProfileBannerEditorModel(teacher.ProfileBanner);
            TeacherId = teacher.Id;
        }
        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public int TeacherId { get; set; }

        public void PopulateEntity(
            Teacher entity,
            IImageService imageService)
        {
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
        }
    }
}