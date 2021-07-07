using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IVenueService
    {
        IList<Venue> LuckyDip();
        IList<Entities.VenueService> GetVenueServices();
        IList<VenueType> GetVenueTypes();
        IEnumerable<VenueAmenity> GetVenueAmenities();
        VenueType GetVenueType(int id);
        Entities.VenueService GetVenueService(int id);
        VenueAmenity GetVenueAmenity(int id);
        Venue GetVenue(int id);
        List<Venue> GetVenues(List<int> ids);
        Venue GetVenueForUser(int id);
        Venue GetVenueForDisplay(int id);
        Venue GetVenueForEditing(int id);
        IList<TeacherPlacement> GetTeacherPlacements(Venue venue);
        Venue CreateVenue(User owner);
        Venue CreateStubbed(string name);
        Venue CreateStubbed(string name, Location location);
        Venue CreateStubbed(string name, Location location, string emailAddress);
        void AddVenue(Venue venue);
        void AddPlacement(Venue venue, Teacher teacher);
        void RemovePlacement(TeacherPlacement placement);
    }
}