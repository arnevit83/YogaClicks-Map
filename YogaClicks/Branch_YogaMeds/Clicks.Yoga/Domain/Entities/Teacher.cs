using System;
using System.Collections.Generic;
using Clicks.Yoga.Permissions.Providers;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class Teacher : PrincipalEntity, IActor, IClaimable, IFanable, IGalleryAssociate, INamed, IReviewable, ISearchable, IProfileBanner
    {
        public Teacher()
        {
            Websites = new List<Website>();
            Styles = new List<Style>();
            Services = new List<TeacherService>();
            Placements = new List<TeacherPlacement>();
            Accreditations = new List<TeacherAccreditation>();
            Conditions = new List<Condition>();
        }

        public string Name { get; set; }

        public string Philosophy { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Website> Websites { get; private set; } 

        public virtual Website Blog { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public virtual ICollection<Style> Styles { get; private set; }

        public virtual ICollection<TeacherService> Services { get; private set; }

        public virtual ICollection<TeacherPlacement> Placements { get; private set; }

        public virtual ICollection<TeacherAccreditation> Accreditations { get; private set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public virtual ICollection<UserStory> UserStories { get; set; }

        public bool NewsletterOptOut { get; set; }

        public bool Stubbed { get; set; }

        public IEnumerable<T> FilterReviewDetailTypes<T>(IEnumerable<T> types) where T : IReviewDetailType
        {
            return types;
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

            record.Location = Location != null ? Location.Coordinates : null;
            record.City = Location != null ? Location.Name : null;

            record.UpdateEntities(new List<IEnumerable<INamed>> {Styles, Placements.Select(p => p.Venue), Services, Accreditations.Select(a => a.AccreditationBody), Conditions});

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
            get { return Location != null ? Location.Name : null; }
        }

        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }
    }
}