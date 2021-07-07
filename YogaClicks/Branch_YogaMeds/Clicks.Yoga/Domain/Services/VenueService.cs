using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.MailingLists;

namespace Clicks.Yoga.Domain.Services
{
    public class VenueService : ServiceBase, IVenueService
    {
        public VenueService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IImageService imageService,
            ISearchService searchService,
            IRequestService requestService,
            IMailingListProvider mailingListProvider)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            ImageService = imageService;
            SearchService = searchService;
            RequestService = requestService;
            MailingListProvider = mailingListProvider;
        }

        private IEntityService EntityService { get; set; }

        private IImageService ImageService { get; set; }

        private ISearchService SearchService { get; set; }

        private IRequestService RequestService { get; set; }

        private IMailingListProvider MailingListProvider { get; set; }

        public IList<Venue> LuckyDip()
        {
            return EntityContext.Venues.OrderBy(v => Guid.NewGuid()).Take(5).ToList();
        }

        public IList<Entities.VenueService> GetVenueServices()
        {
            return EntityContext.VenueServices.OrderBy(e => e.Name).ToList();
        }

        public IList<VenueType> GetVenueTypes()
        {
            return EntityContext.VenueTypes.OrderBy(e => e.Name).ToList();
        }

        public IEnumerable<VenueAmenity> GetVenueAmenities()
        {
            return EntityContext.VenueAmenities.OrderBy(e => e.Name).ToList();
        }

        public VenueType GetVenueType(int id)
        {
            return EntityContext.VenueTypes.Get(id);
        }

        public Entities.VenueService GetVenueService(int id)
        {
            return EntityContext.VenueServices.Get(id);
        }

        public VenueAmenity GetVenueAmenity(int id)
        {
            return EntityContext.VenueAmenities.Get(id);
        }

        public Venue GetVenue(int id)
        {
            return EntityContext.Venues.Get(id);
        }

        public List<Venue> GetVenues(List<int> ids)
        {
            return EntityContext.Venues
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }

        public Venue GetVenueForUser(int id)
        {
            return EntityContext.Venues.Get(e => e.Active && e.Owner.Id == id);
        }

        public Venue GetVenueForDisplay(int id)
        {
            var venue = EntityContext.Venues.Get(id);
            if (venue == null) throw new UnknownEntityException();
            return venue;
        }

        public Venue GetVenueForEditing(int id)
        {
            var venue = GetVenueForDisplay(id);
            if (!SecurityContext.CanUpdate(venue)) throw new PermissionDeniedException();
            return venue;
        }

        public IList<TeacherPlacement> GetTeacherPlacements(Venue venue)
        {
            return EntityContext.TeacherPlacements
                .Where(e => e.Venue.Id == venue.Id)
                .Where(e => e.Venue.Active)
                .ToList();
        }

        public Venue CreateVenue(User owner)
        {
            var venue = new Venue { Owner = owner };
            AddVenue(venue);
            return venue;
        }

        public Venue CreateStubbed(string name)
        {
            var venue = new Venue { Name = name, Stubbed = true };

            EntityContext.Venues.Add(venue);

            return venue;
        }

        public Venue CreateStubbed(string name, Location location)
        {
            var venue = CreateStubbed(name);
            venue.Address = new Address { Location = location.Coordinates };
            return venue;
        }

        public Venue CreateStubbed(string name, Location location, string emailAddress)
        {
            var venue = CreateStubbed(name);
            venue.Address = new Address { Location = location.Coordinates };
            venue.EmailAddress = emailAddress;
            return venue;
        }

        public void AddVenue(Venue venue)
        {
            EntityContext.Venues.Add(venue);

            if (venue.Owner != null)
            {
                MailingListProvider.AddToGroup(MailingList.Newsletter, venue.Owner.EmailAddress, "Type", "Venue");
            }
        }

        public void AddPlacement(Venue venue, Teacher teacher)
        {
            if (venue == null)
                throw new ArgumentNullException("venue");
            if (teacher == null)
                throw new ArgumentNullException("teacher");

            var placement = new TeacherPlacement
            {
                Teacher = teacher,
                Venue = venue,
                Confirmed = venue.Owner == null || teacher.Owner == null || venue.Owner.Id == teacher.Owner.Id
            };

            teacher.Placements.Add(placement);

            if (venue.Owner != null && teacher.Owner != null && venue.Owner.Id != teacher.Owner.Id)
            {
                RequestService.Request("VenuePlacementAdded", teacher.Owner, SecurityContext.CurrentActor, venue, teacher, placement);
            }
        }

        public void RemovePlacement(TeacherPlacement placement)
        {
            if (placement == null)
                throw new ArgumentNullException("placement");

            EntityContext.TeacherPlacements.Remove(placement);
        }
    }
}