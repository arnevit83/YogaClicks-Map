namespace Clicks.Yoga.Domain.Entities
{
    public interface INewsLink
    {
        bool NewsRequired { get; }
        Profile NewsSubscriber { get; }
        IEntityHandle NewsSubject { get; }
    }
}