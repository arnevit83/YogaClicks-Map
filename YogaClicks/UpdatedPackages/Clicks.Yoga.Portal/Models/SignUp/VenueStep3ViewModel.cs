using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class VenueStep3ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Description { get; set; }

        public SignUpWebsiteCollectionEditorModel Websites { get; set; }

        public SignUpAddressEditorModel Address { get; set; }

        public SignUpProfileBannerEditorModel ProfileBanner { get; set; }

        public SignUpProfileImageEditorModel Image { get; set; }

        public bool IsEdit { get; set; }

        public Profile UserProfile { get; set; }

        public VenueStep3ViewModel()
        {
            Address = new SignUpAddressEditorModel();
            Websites = new SignUpWebsiteCollectionEditorModel();
        }

        public VenueStep3ViewModel(IList<Country> countries)
        {
            Address = new SignUpAddressEditorModel();
            Address.PopulateMetadata(countries);
            Websites = new SignUpWebsiteCollectionEditorModel();
        }

        public VenueStep3ViewModel(Venue venue, IList<Country> countries, Profile profile)
        {
            Id = venue.Id;
            Name = venue.Name;
            Telephone = venue.Telephone;
            Description = venue.Description;
            Image = new SignUpProfileImageEditorModel(venue.Image);
            ProfileBanner = new SignUpProfileBannerEditorModel(venue.ProfileBanner);
            Websites = new SignUpWebsiteCollectionEditorModel(venue.Websites);
            Address = new SignUpAddressEditorModel(venue.Address);
            Address.PopulateMetadata(countries);
            IsEdit = true;
            UserProfile = profile;
        }

        public void PopulateEntity(Venue venue, IWebsiteService websiteService, IImageService imageService, ITeacherService teacherService, IAddressService addressService)
        {
            venue.Name = Name;
            venue.Telephone = Telephone;
            venue.Description = Description;
            Websites.PopulateCollection(venue.Websites, websiteService);
            venue.Image = Image.PopulateEntity(() => venue.Image, imageService);
            venue.ProfileBanner = ProfileBanner.PopulateEntity(() => venue.ProfileBanner, imageService);
            venue.Address = Address.PopulateEntity(venue.Address, addressService);

        }
    }
}