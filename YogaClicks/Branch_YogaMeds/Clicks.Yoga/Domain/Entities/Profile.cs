using System;
using System.Collections.Generic;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class Profile : PrincipalEntity, IActor, IGalleryAssociate, INamed, ISearchable, IProfileBanner
    {
        public Profile()
        {
            Websites = new List<Website>();
            ProfileAnswers = new List<ProfileAnswer>();
            Friendships = new List<Friendship>();
            Public = true;
            IsFirstVisit = true;
        }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public virtual Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string EmailAddress { get; set; }

        public virtual ICollection<Website> Websites { get; private set; } 

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public virtual ICollection<ProfileAnswer> ProfileAnswers { get; private set; }

        public bool IsFirstVisit { get; set; }

        public bool ThirdPartyOptOut { get; set; }

        public bool NewsletterOptOut { get; set; }

        public bool Public { get; set; }

        public bool Professional { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Friendship> Friendships { get; private set; }

        public virtual ProfileWall Wall { get; set; }

        public string Name
        {
            get { return string.Format("{0} {1}", Forename, Surname); }
        }

        public bool IsSearchable
        {
            get { return Active && Public; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Description = "";
            record.Image = Image;
            record.Location = Location != null ? Location.Coordinates : null;
            record.City = Location != null ? Location.Name : null;
        }

        public Type GetGalleryAssociatePermissionProviderType()
        {
            return typeof(DefaultGalleryPermissionProvider);
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