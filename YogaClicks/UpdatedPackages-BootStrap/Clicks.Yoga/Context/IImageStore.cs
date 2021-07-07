using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Context
{
    public interface IImageStore
    {
        void SaveImage(Image image);
    }
}