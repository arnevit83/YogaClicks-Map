using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Image = Clicks.Yoga.Domain.Entities.Image;

namespace Clicks.Yoga.Domain.Services
{
    public class ImageService : ServiceBase, IImageService
    {
        public ImageService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) { }

        public bool ValidImageStream(Stream stream)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(stream);
                image.Dispose();
                stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Image GetImage(int id)
        {
            return EntityContext.Images.Get(id);
        }

        public ImageType GetImageType(int id)
        {
            return EntityContext.ImageTypes.Get(id);
        }

        public ImageType GetImageType(string mimeType)
        {
            return EntityContext.ImageTypes.Get(e => e.MimeType == mimeType);
        }

        public Image CreateImage(PropertyInfo property, Stream stream, string contentType)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (!ValidImageStream(stream))
                throw new ArgumentException("The stream does not represent a valid image.", "stream");

            var type = GetImageType(contentType);

            if (type == null)
                throw new ArgumentOutOfRangeException("contentType");

            var image = new Image(property, stream, type, SecurityContext.CurrentUser) { Temporary = true };

            EntityContext.Images.Add(image);

            return image;
        }

        public Image CreateTemporaryImage(PropertyInfo property, Stream stream, string contentType)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (!ValidImageStream(stream))
                throw new ArgumentException("The stream does not represent a valid image.", "stream");

            var image = CreateImage(property, stream, contentType);

            image.Temporary = true;

            return image;
        }

        public Image CreateImage(PropertyInfo property, System.Drawing.Image bitmap)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var contentType = GetContentType(bitmap);
            var stream = new MemoryStream();

            bitmap.Save(stream, bitmap.RawFormat);

            return CreateImage(property, stream, contentType);
        }

        public Image ReplaceImage(PropertyInfo property, Image currentImage, Stream stream, string contentType)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (!ValidImageStream(stream))
                throw new ArgumentException("The stream does not represent a valid image.", "stream");

            if (currentImage != null) currentImage.Active = false;

            var type = GetImageType(contentType);

            if (type == null)
                throw new ArgumentOutOfRangeException("contentType");

            var image = new Image(property, stream, type, SecurityContext.CurrentUser);

            EntityContext.Images.Add(image);

            return image;
        }

        public Image ReplaceImage(Image currentImage, int id)
        {
            var newImage = GetTemporaryImage(id);

            if (newImage == null) return currentImage;

            newImage.Temporary = false;

            if (currentImage == null) currentImage.Active = false;

            return newImage;
        }

        private Image GetTemporaryImage(int id)
        {
            if (!SecurityContext.Authenticated) return null;

            var image = EntityContext.Images.Get(e => e.Id == id && e.Temporary);

            if (image != null && image.Owner.Id != SecurityContext.CurrentUser.Id) image = null;

            return image;
        }

        public void AddImage(Image image)
        {
            EntityContext.Images.Add(image);
        }

        public void DeleteImage(int id)
        {
            var image = EntityContext.Images.Get(id);

            if (image == null)
                throw new ArgumentOutOfRangeException("id");
            if (!SecurityContext.CanDelete(image))
                throw new PermissionDeniedException();

            EntityContext.Images.Remove(image);
        }

        //Method to crop an image stream to certain dimensions dictated by ImageCrop Object
        public Stream CropImage(Stream imageStream, ImageCrop imageCrop)
        {
            var bmpImage = new Bitmap(imageStream);
            var imgCanvas = new Bitmap((int)imageCrop.Width, (int)imageCrop.Height);

            using (var g = Graphics.FromImage(imgCanvas))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmpImage, new Rectangle(0, 0, (int)imageCrop.Width, (int)imageCrop.Height), (int)imageCrop.XCoord, (int)imageCrop.YCoord, (int)imageCrop.Width, (int)imageCrop.Height, GraphicsUnit.Pixel);
            }

            var ms = new MemoryStream();
            //Save as JPeg to ensure maximum space savings
            imgCanvas.Save(ms, ImageFormat.Jpeg);

            return ms;
		}
		
        private string GetContentType(System.Drawing.Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            return ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == image.RawFormat.Guid).MimeType;
        }
    }
}