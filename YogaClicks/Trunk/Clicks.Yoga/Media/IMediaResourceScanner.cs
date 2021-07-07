using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Media
{
    public interface IMediaResourceScanner
    {
        void Scan(MediaResource resource);
        void Commit(MediaResource resource);
    }
}