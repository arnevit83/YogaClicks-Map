namespace Clicks.Yoga.Domain.Entities
{
    public interface IEntityHandle : IEntity
    {
        string Name { get; }
        Image Image { get; }
        string Location { get; }
        User Owner { get; }
        bool Active { get; }
    }
}
