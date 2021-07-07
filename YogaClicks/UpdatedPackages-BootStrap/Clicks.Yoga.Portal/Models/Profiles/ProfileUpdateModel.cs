using System;
using System.Collections.Generic;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileUpdateModel
    {
        public ProfileUpdateModel()
        {
            Gender = new GenderSelectorModel();
            BirthDate = new DateEditorModel { Optional = true };
            BirthDate.MinmumYear = DateTime.Now.Year - 150;
            BirthDate.Maximum = DateTime.Now.Date;
            Location = new LocationEditorModel();
            Websites = new WebsiteCollectionEditorModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
        }

        public ProfileUpdateModel(Profile profile, IEnumerable<ProfileQuestion> questions, IEnumerable<ProfileAnswer> answers)
        {
            ProfileId = profile.Id;
            Profile = new ProfileModel(profile);
            Forename = profile.Forename;
            Surname = profile.Surname;
            Gender = new GenderSelectorModel(profile.Gender);
            BirthDate = new DateEditorModel(profile.BirthDate) { Optional = true };
            BirthDate.MinmumYear = DateTime.Now.Year - 150;
            BirthDate.Maximum = DateTime.Now.Date;
            EmailAddress = profile.EmailAddress;
            Location = new LocationEditorModel(profile.Location);
            Websites = new WebsiteCollectionEditorModel(profile.Websites);
            Image = new ProfileImageEditorModel(profile.Image);
            ProfileBanner = new ProfileBannerEditorModel(profile.ProfileBanner);
            ProfileQuestions = new ProfileQuestionCaptureModel(questions, answers);
            NewsletterOptOut = profile.NewsletterOptOut;

            Location.ShowCityOnly = true;
        }

        public int ProfileId { get; set; }

        public ProfileModel Profile { get; private set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public GenderSelectorModel Gender { get; set; }

        public DateEditorModel BirthDate { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteCollectionEditorModel Websites { get; set; }

        public LocationEditorModel Location { get; set; }

        public ProfileImageEditorModel Image { get; set; }

        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public ProfileQuestionCaptureModel ProfileQuestions { get; set; }

        public bool NewsletterOptOut { get; set; }

        public bool ThirdPartyOptOut { get; set; }

        public void PopulateEntity(
            Profile profile,
            ISecurityContext securityContext,
            IProfileService profileService,
            IWebsiteService websiteService,
            IImageService imageService)
        {
            profile.Forename = Forename;
            profile.Surname = Surname;
            profile.BirthDate = BirthDate.DateTime;
            profile.Gender = Gender.Selected ? profileService.GetGender(Gender.Id) : null;
            profile.EmailAddress = EmailAddress;
            profile.NewsletterOptOut = NewsletterOptOut;
            profile.ThirdPartyOptOut = ThirdPartyOptOut;
            profile.Location = Location.PopulateEntity(profile.Location);
            profile.Image = Image.PopulateEntity(() => profile.Image, imageService);
            profile.ProfileBanner = ProfileBanner.PopulateEntity(() => profile.ProfileBanner, imageService);

            Websites.PopulateCollection(profile.Websites, websiteService);
        }
    }
}