using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ProfileModel : PrincipalEntityModel<Profile>
    {
        public ProfileModel(Profile entity) : base(entity) {}

        public bool Professional { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public GenderModel Gender { get; set; }

        public string EmailAddress { get; set; }

        public LocationModel Location { get; set; }

        public ImageModel Image { get; set; }

        public ImageModel ProfileBanner { get; set; }

        public IList<WebsiteModel> Websites { get; private set; } 

        public IList<ProfileAnswerModel> ProfileAnswers { get; private set; }

        public WallModel Wall { get; set; }

        public override void Populate(Profile entity)
        {
            Id = entity.Id;
            Professional = entity.Professional;
            Forename = entity.Forename;
            Surname = entity.Surname;
            Name = entity.Name;
            BirthDate = entity.BirthDate;
            Gender = new GenderModel(entity.Gender);
            EmailAddress = entity.EmailAddress;
            Location = new LocationModel(entity.Location);
            Image = new ImageModel(entity.Image);
            ProfileBanner = new ImageModel(entity.ProfileBanner);
            Wall = new WallModel(entity.Wall);
            Websites = new List<WebsiteModel>();
            ProfileAnswers = new List<ProfileAnswerModel>();

            foreach (var website in entity.Websites)
            {
                Websites.Add(new WebsiteModel(website));
            }

            foreach (var answer in entity.ProfileAnswers)
            {
                ProfileAnswers.Add(new ProfileAnswerModel(answer));
            }
        }
    }
}