using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    [DataContract]
    public class ProfileStep2ViewModel
    {
        public int Id { get; set; }

        [DataMember]
        public string Forename { get; set; }

        [DataMember]
        public string Surname { get; set; }

        public SignUpDateEditorModel BirthDate { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string Password { get; set; }

        public SignUpLocationEditorModel Location { get; set; }

        public SignUpProfileBannerEditorModel ProfileBanner { get; set; }

        public SignUpProfileImageEditorModel Image { get; set; }

        public TypeOfSignUp TypeOfSignUpValue { get; set; }

        public bool IsEdit { get; set; }

        public enum TypeOfSignUp
        {
            Profile,
            Teacher,
            Venue,
            Tto
        }

        public void PopulateViewModel(Profile profile)
        {
            Forename = profile.Forename;
            Surname = profile.Surname;
            Password = "FakePassword1";
            BirthDate = new SignUpDateEditorModel(profile.BirthDate) { MinmumYear = DateTime.Now.Year - 150, MaximumYear = DateTime.Now.Year};
            EmailAddress = profile.EmailAddress;
            Location = new SignUpLocationEditorModel(profile.Location);
            Image = new SignUpProfileImageEditorModel(profile.Image);
            ProfileBanner = new SignUpProfileBannerEditorModel(profile.ProfileBanner);
            IsEdit = true;
        }

        public void PopulateEntity(Profile profile, IWebsiteService websiteService, IProfileService profileService, IImageService imageService)
        {
            profile.Forename = Forename;
            profile.Surname = Surname;
            profile.BirthDate = BirthDate.DateTime;
            profile.EmailAddress = EmailAddress;
            profile.Location = Location.PopulateEntity(profile.Location);
            profile.Image = Image.PopulateEntity(() => profile.Image, imageService);
            profile.ProfileBanner = ProfileBanner.PopulateEntity(() => profile.ProfileBanner, imageService);
        }
    }
}