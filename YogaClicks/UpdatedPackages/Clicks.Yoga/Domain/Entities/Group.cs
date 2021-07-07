using System;
using System.Collections.Generic;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class Group : PrincipalEntity, IEntityHandle, IGalleryAssociate, INamed, ISearchable, IProfileBanner
    {
        public Group()
        {
            Styles = new List<Style>();
            Members = new List<GroupMember>();
            Professions = new List<EntityType>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual EntityHandle PromoterHandle { get; set; }

        public virtual Image Image { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public virtual ICollection<Style> Styles { get; private set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public virtual ICollection<GroupMember> Members { get; private set; }

        public bool Public { get; set; }

        public bool Professional { get; set; }

        public bool ProfessionsRestricted { get; set; }

        public virtual ICollection<EntityType> Professions { get; private set; }

        public virtual GroupWall Wall { get; set; }

        public bool MemberInvitationsPermitted { get; set; }

        public bool MemberPostingPermitted { get; set; }

        public bool MemberGalleriesPermitted { get; set; }

        public bool AutoJoin { get; set; }

        public bool IsGlobalFaqs { get; set; }

        public bool IsSearchable
        {
            get { return Active && Public; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Description = Description ?? "";
            record.Image = Image;

            record.UpdateEntities(new List<IEnumerable<INamed>> { Styles});

        }

        public IEnumerable<ISearchable> GetChildSearchables()
        {
            yield break;
        }

        public Type GetGalleryAssociatePermissionProviderType()
        {
            return typeof(GroupGalleryPermissionProvider);
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }
    }
}