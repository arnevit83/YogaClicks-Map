namespace Clicks.Yoga.Domain.Entities
{
    public class GroupMember : Entity, INewsLink
    {
        public GroupMember() {}

        public GroupMember(Group group, User user, bool administrator, bool confirmed)
        {
            Group = group;
            User = user;
            Administrator = administrator;
            Confirmed = confirmed;
        }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }

        public bool Administrator { get; set; }

        public bool Confirmed { get; set; }

        bool INewsLink.NewsRequired
        {
            get { return Confirmed; }
        }

        Profile INewsLink.NewsSubscriber
        {
            get { return User.Profile; }
        }

        IEntityHandle INewsLink.NewsSubject
        {
            get { return Group; }
        }
    }
}