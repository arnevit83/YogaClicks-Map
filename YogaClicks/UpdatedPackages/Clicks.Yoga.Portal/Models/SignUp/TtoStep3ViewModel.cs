using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class TtoStep3ViewModel
    {
        public int Id { get; set; }

        public Profile UserProfile { get; set; }

        public string Name { get; set; }

        public string Founder { get; set; }

        public string Email { get; set; }

        public WebsiteEditorModel Website { get; set; }

        public SignUpAddressEditorModel Address { get; set; }

        public SignUpProfileBannerEditorModel ProfileBanner { get; set; }

        public SignUpProfileImageEditorModel Image { get; set; }

        public bool IsEdit { get; set; }

        public TtoStep3ViewModel()
        {
            Address = new SignUpAddressEditorModel();
            Website = new WebsiteEditorModel();
        }

        public TtoStep3ViewModel(IList<Country> countries)
        {
            Website = new WebsiteEditorModel();
            Address = new SignUpAddressEditorModel();
            Address.PopulateMetadata(countries);
        }

        public TtoStep3ViewModel(TeacherTrainingOrganisation tto, IList<Country> countries, Profile userProfile)
        {
            Id = tto.Id;
            Name = tto.Name;
            Founder = tto.Founder;
            Email = tto.EmailAddress;
            Website = new WebsiteEditorModel(tto.Website);
            Image = new SignUpProfileImageEditorModel(tto.Image);
            ProfileBanner = new SignUpProfileBannerEditorModel(tto.ProfileBanner);
            Address = new SignUpAddressEditorModel(tto.Address);
            Address.PopulateMetadata(countries);
            IsEdit = true;
            UserProfile = userProfile;
        }

        public void PopulateEntity(TeacherTrainingOrganisation tto, User user, IWebsiteService websiteService, IImageService imageService, IAddressService addressService)
        {
            tto.Owner = user;
            tto.Name = Name;
            tto.Founder = Founder;
            tto.EmailAddress = Email;
            tto.Website = Website.PopulateEntity(tto.Website, websiteService);
            tto.Image = Image.PopulateEntity(() => tto.Image, imageService);
            tto.ProfileBanner = ProfileBanner.PopulateEntity(() => tto.ProfileBanner, imageService);
            tto.Address = Address.PopulateEntity(tto.Address, addressService);

        }
    }
}