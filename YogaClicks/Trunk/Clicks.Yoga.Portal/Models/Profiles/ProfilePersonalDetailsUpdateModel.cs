using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfilePersonalDetailsUpdateModel
    {
        public ProfilePersonalDetailsUpdateModel()
        {
            Gender = new GenderSelectorModel();
        }

        public ProfilePersonalDetailsUpdateModel(Profile profile)
        {
            ProfileId = profile.Id;
            Profile = new ProfileModel(profile);
            Forename = profile.Forename;
            Surname = profile.Surname;
            BirthDate = new DateEditorModel(profile.BirthDate) { Optional = true };
            BirthDate.MinmumYear = DateTime.Now.Year - 150;
            BirthDate.Maximum = DateTime.Now.Date;
            EmailAddress = profile.EmailAddress;
            Websites = new WebsiteCollectionEditorModel(profile.Websites);
            Gender = new GenderSelectorModel(profile.Gender);
        }
        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateEditorModel BirthDate { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteCollectionEditorModel Websites { get; set; }

        public int ProfileId { get; set; }

        public ProfileModel Profile { get; private set; }

        public GenderSelectorModel Gender { get; set; }

        public void PopulateEntity(
            Profile profile,
             IWebsiteService websiteService,
            IProfileService profileService)
        {
            profile.Forename = Forename;
            profile.Surname = Surname;
            profile.BirthDate = BirthDate.DateTime;
            profile.EmailAddress = EmailAddress;
            Websites.PopulateCollection(profile.Websites, websiteService);
            profile.Gender = Gender.Selected ? profileService.GetGender(Gender.Id) : null;
        }
    }
}