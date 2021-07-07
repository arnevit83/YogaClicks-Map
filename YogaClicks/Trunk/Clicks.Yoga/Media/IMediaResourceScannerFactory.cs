using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Media
{
    public interface IMediaResourceScannerFactory
    {
        IMediaResourceScanner GetHandler(MediaResourceType type);
    }
}