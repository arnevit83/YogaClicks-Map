using System.IO;
using System.Reflection;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IImageService
    {
        bool ValidImageStream(Stream stream);
        Image GetImage(int id);
        ImageType GetImageType(int id);
        ImageType GetImageType(string mimeType);
        Image CreateImage(PropertyInfo property, Stream stream, string contentType);
        Image CreateTemporaryImage(PropertyInfo property, Stream stream, string contentType);
        Image CreateImage(PropertyInfo property, System.Drawing.Image bitmap);
        Image ReplaceImage(PropertyInfo property, Image currentImage, Stream stream, string contentType);
        Image ReplaceImage(Image currentImage, int id);
        void AddImage(Image image);
        void DeleteImage(int id);
        Stream CropImage(Stream image, ImageCrop imageCrop);
    }
}