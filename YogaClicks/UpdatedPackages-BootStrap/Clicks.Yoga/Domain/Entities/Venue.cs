using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class Venue : PrincipalEntity, IActor, IClaimable, IFanable, IGalleryAssociate, INamed, IReviewable, ISearchable, IProfileBanner
    {
        public Venue()
        {
            Websites = new List<Website>();
            Styles = new List<Style>();
            Services = new List<VenueService>();
            Amenities = new List<VenueAmenity>();
            TeacherPlacements = new List<TeacherPlacement>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public virtual VenueType Type { get; set; }

        public bool IsResidential
        {
            get { return Type == null || Type.IsResidential; }
        }

        public virtual Address Address { get; set; }

        public virtual ICollection<Website> Websites { get; private set; }

        public virtual Website Blog { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public virtual ICollection<Style> Styles { get; private set; }

        public virtual ICollection<VenueService> Services { get; private set; }

        public virtual ICollection<VenueAmenity> Amenities { get; private set; }

        public virtual ICollection<TeacherPlacement> TeacherPlacements { get; private set; }

        public bool NewsletterOptOut { get; set; }

        public bool Stubbed { get; set; }

        public IEnumerable<T> FilterReviewDetailTypes<T>(IEnumerable<T> types) where T : IReviewDetailType
        {
            var filterKey = (Type != null && Type.IsResidential) ? "Residential" : "NonResidential";
            return types.Where(e => e.FilterKey == null || e.FilterKey == filterKey);
        }

        public IEnumerable<Tuple<string, string>> GetReviewHelperDetails()
        {
            yield break;
        }

        public bool IsSearchable
        {
            get { return Active; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Description = "";
            record.Image = Image;
            record.Stubbed = Stubbed;

            record.City = Address != null && Address.City != null ? Address.City : "";
            record.Area = Address != null && Address.Area != null ? Address.Area : "";
            record.Country = Address != null && Address.Country != null ? Address.Country.EnglishName : "";
            record.Postcode = Address != null && Address.Postcode != null ? Address.Postcode : "";
            record.Location = Address != null ? Address.Location : null;

            record.VenueType = Type;

            var types = Type != null ? new List<VenueType> {Type} : new List<VenueType>();

            record.UpdateEntities(new List<IEnumerable<INamed>> { Styles, TeacherPlacements.Select(p => p.Teacher), Services, types });

        }

        public Type GetGalleryAssociatePermissionProviderType()
        {
            return typeof(DefaultGalleryPermissionProvider);
        }

        bool IClaimable.Stubable
        {
            get { return true; }
        }

        string IEntityHandle.Location
        {
            get { return Address != null ? Address.LocationName : ""; }
        }

        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }
    }
}