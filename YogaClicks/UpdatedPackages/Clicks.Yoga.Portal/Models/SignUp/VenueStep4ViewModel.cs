using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Teachers;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class VenueStep4ViewModel
    {
        public int Id { get; set; }

        public bool IsExistingVenue { get; set; }

        public int TeacherId { get; set; }

        public SignUpVenueTypeSelectorModel Type { get; private set; }

        public SignUpVenueServiceChooserModel Services { get; set; }

        public SignUpVenueAmenityChooserModel ResidentialAmenities { get; private set; }

        public SignUpVenueAmenityChooserModel Amenities { get; private set; }

        public SignUpStyleChooserModel Styles { get; set; }

        public SignUpConditionChooserModel Conditions { get; set; }

        public bool IsEdit { get; set; }
        
        public VenueStep4ViewModel()
        {
            Services = new SignUpVenueServiceChooserModel();
            Styles = new SignUpStyleChooserModel();
            Conditions = new SignUpConditionChooserModel();
            Type = new SignUpVenueTypeSelectorModel();

            ResidentialAmenities = new SignUpVenueAmenityChooserModel();
            Amenities = new SignUpVenueAmenityChooserModel();
        }


        public VenueStep4ViewModel(Venue venue, IVenueService venueService, IStyleService styleService, IMedicService medicService)
        {
            Id = venue.Id;
            Services = new SignUpVenueServiceChooserModel(venue.Services);
            Styles = new SignUpStyleChooserModel(venue.Styles);
            Conditions = new SignUpConditionChooserModel(venue.Conditions);
            ResidentialAmenities = new SignUpVenueAmenityChooserModel(venue.Amenities);
            Amenities = new SignUpVenueAmenityChooserModel(venue.Amenities);
            Type = new SignUpVenueTypeSelectorModel(venue.Type);

            Services.PopulateMetadata(venueService.GetVenueServices());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
            ResidentialAmenities.PopulateMetadata(venueService.GetVenueAmenities(), true);
            Amenities.PopulateMetadata(venueService.GetVenueAmenities(), false);
            Type.PopulateMetadata(venueService.GetVenueTypes());
            IsEdit = true;
        }
        
        public void PopulateEntity(Venue venue, IStyleService styleService, IVenueService venueService,
            IMedicService medicService)
        {
            Services.PopulateCollection(venue.Services, venueService);
            Styles.PopulateCollection(venue.Styles, styleService);
            Conditions.PopulateCollection(venue.Conditions, medicService);
            venue.Type = Type.Selected ? venueService.GetVenueType(Type.Id) : null;

            if (venue.IsResidential)
            {
                ResidentialAmenities.PopulateCollection(venue.Amenities, venueService, venue);
            }
            else
            {
                Amenities.PopulateCollection(venue.Amenities, venueService, venue);
            }

        }
    }
}