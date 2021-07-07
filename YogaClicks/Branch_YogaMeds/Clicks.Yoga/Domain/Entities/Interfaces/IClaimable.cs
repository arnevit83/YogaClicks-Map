namespace Clicks.Yoga.Domain.Entities
{
    public interface IClaimable
    {
        User Owner { get; set; }
        bool Stubable { get; }
        bool Stubbed { get; set; }
    }
}