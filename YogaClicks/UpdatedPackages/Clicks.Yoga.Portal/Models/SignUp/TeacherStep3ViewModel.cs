using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class TeacherStep3ViewModel
    {
        public int Id { get; set; }

        public Profile UserProfile { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public SignUpWebsiteCollectionEditorModel Websites { get; set; }

        public SignUpLocationEditorModel Address { get; set; }

        public SignUpProfileBannerEditorModel ProfileBanner { get; set; }

        public SignUpProfileImageEditorModel Image { get; set; }

        public bool IsEdit { get; set; }

        public TeacherStep3ViewModel()
        {
            Address = new SignUpLocationEditorModel();
            Websites = new SignUpWebsiteCollectionEditorModel();
        }

        public TeacherStep3ViewModel(IList<Country> countries)
        {
            Websites = new SignUpWebsiteCollectionEditorModel();
            Address = new SignUpLocationEditorModel();
        }

        public TeacherStep3ViewModel(Teacher teacher, IList<Country> countries, Profile profile)
        {
            Id = teacher.Id;
            Name = teacher.Name;
            Telephone = teacher.Telephone;
            Image = new SignUpProfileImageEditorModel(teacher.Image);
            ProfileBanner = new SignUpProfileBannerEditorModel(teacher.ProfileBanner);
            Websites = new SignUpWebsiteCollectionEditorModel(teacher.Websites);
            Address = new SignUpLocationEditorModel(teacher.Location);
            IsEdit = true;
            UserProfile = profile;
        }

        public void PopulateEntity(Teacher teacher, IWebsiteService websiteService, IImageService imageService, ITeacherService teacherService)
        {
            teacher.Name = Name;
            teacher.Telephone = Telephone;
            Websites.PopulateCollection(teacher.Websites, websiteService);
            teacher.Image = Image.PopulateEntity(() => teacher.Image, imageService);
            teacher.ProfileBanner = ProfileBanner.PopulateEntity(() => teacher.ProfileBanner, imageService);
            teacher.Location = Address.PopulateEntity(teacher.Location);

        }
    }
}