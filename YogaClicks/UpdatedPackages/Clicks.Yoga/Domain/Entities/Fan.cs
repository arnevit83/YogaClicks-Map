namespace Clicks.Yoga.Domain.Entities
{
    public class Fan : Entity, INewsLink
    {
        public virtual User User { get; set; }

        public virtual EntityHandle EntityHandle { get; set; }

        bool INewsLink.NewsRequired
        {
            get { return true; }
        }

        Profile INewsLink.NewsSubscriber
        {
            get { return User.Profile; }
        }

        IEntityHandle INewsLink.NewsSubject
        {
            get { return EntityHandle; }
        }
    }
}