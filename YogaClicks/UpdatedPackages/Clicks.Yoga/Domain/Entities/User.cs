using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class User : Entity, ISecurable
    {
        public User()
        {
            Roles = new List<UserRole>();
            PasswordLogins = new List<PasswordLogin>();
            Memberships = new List<GroupMember>();
        }

        public string EmailAddress { get; set; }

        public bool Active { get; set; }

        public virtual Profile Profile { get; set; }

        public bool IsSuperUser { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<EntityHandle> Owned { get; set; }

        public virtual ICollection<PasswordLogin> PasswordLogins { get; private set; } 

        public virtual ICollection<GroupMember> Memberships { get; private set; }

  
        public virtual NotificationPreferences NotificationPreferences { get; set; }

        public virtual PrivacyPreferences PrivacyPreferences { get; set; }

        public bool IsOwner(ISecurable securable)
        {
            if (securable == null || !securable.OwnerId.HasValue) return false;
            return Id == securable.OwnerId;
        }

        public bool CanUpdate(ISecurable securable)
        {
            return IsOwnerOrSuperUser(securable);
        }

        public bool CanDelete(ISecurable securable)
        {
            return IsOwnerOrSuperUser(securable);
        }

        public bool CanAttach(ISecurable securable)
        {
            return IsOwnerOrSuperUser(securable);
        }

        private bool IsOwnerOrSuperUser(ISecurable securable)
        {
            return IsSuperUser || IsOwner(securable);
        }

        int? ISecurable.OwnerId
        {
            get { return Id; }
        }
    }
}