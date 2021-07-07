using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Context
{
    public class CurrentProfileModel
    {
        public CurrentProfileModel(Profile profile)
        {
            Id = profile.Id;
            Professional = profile.Professional;
            Name = profile.Name;
            Image = new ImageModel(profile.Image);
            Email = profile.EmailAddress;
            ProfileAddress = profile.Location ?? new Location();
            CreateDate = profile.CreationTime;
            YogaPro = profile.YogaPro;
            HasaStory = profile.HasStory;
            NumberofLoginIn = profile.NumberofLoginIn;
            LastLoggedIn = profile.LastLoggedIn ?? new DateTime();

             

        }

        public bool YogaPro { get; set; }
        public bool HasaStory { get; set; }
        public int NumberofLoginIn { get; set; }
        public DateTime LastLoggedIn { get; set; }



        public int Id { get; set; }

        public bool Professional { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public string Email { get; set; }

        public Location ProfileAddress { get; set; }
        
        public ImageModel Image { get; private set; }
    }
}