using Clicks.Yoga.Permissions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherTrainingOrganisation : PrincipalEntity, IActor, IClaimable, IFanable, INamed, ISearchable, IProfileBanner, IGalleryAssociate, IReviewable
    {
        public TeacherTrainingOrganisation()
        {
            Styles = new List<Style>();
            Courses = new List<TeacherTrainingCourse>();
        }

        public string Name { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public string Founder { get; set; }

        public virtual Address Address { get; set; }

        public string Description { get; set; }

        public virtual Website Website { get; set; }

        public string EmailAddress { get; set; }

        public bool Stubbed { get; set; }

        public virtual ICollection<Style> Styles { get; private set; } 

        public virtual ICollection<TeacherTrainingCourse> Courses { get; private set; }

        public bool IsSearchable
        {
            get { return Active; }
        }

        public IEnumerable<T> FilterReviewDetailTypes<T>(IEnumerable<T> types) where T : IReviewDetailType
        {
            return types.Where(e => e.FilterKey == null);
        }

        public IEnumerable<Tuple<string, string>> GetReviewHelperDetails()
        {
            yield break;
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

            record.UpdateEntities(new List<IEnumerable<INamed>> { Styles });

        }

        bool IClaimable.Stubable
        {
            get { return false; }
        }

        bool IClaimable.Stubbed
        {
            get { return false; }
            set {}
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            return Courses;
        }

        public Type GetGalleryAssociatePermissionProviderType()
        {
            return typeof(DefaultGalleryPermissionProvider);
        }
    }
}