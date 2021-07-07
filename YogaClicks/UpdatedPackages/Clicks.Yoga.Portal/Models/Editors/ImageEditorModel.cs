using System;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class ImageEditorModel
    {
        public ImageEditorModel()
        {
            CurrentImage = new ImageModel();
            NewImage = new ImageUploadModel();
            ImageCrop = new ImageCrop();
        }

        public ImageEditorModel(Image image)
        {
            CurrentImage = new ImageModel(image);
            NewImage = new ImageUploadModel();
            ImageCrop = new ImageCrop();
        }

        public ImageModel CurrentImage { get; set; }

        public ImageUploadModel NewImage { get; set; }

        public ImageCrop ImageCrop { get; set; }
        
        public Image PopulateEntity(Expression<Func<Image>> property, IImageService imageService)
        {
            var image = property.Compile().Invoke();

            if (NewImage != null && ImageCrop.ShouldCrop)
            {
                NewImage.CroppedFile = imageService.CropImage(NewImage.PostedFile.InputStream, ImageCrop);
                NewImage.CroppedContentType = "image/jpeg"; //Force to Jpeg Type
            }

            image = NewImage != null ? NewImage.PopulateEntity(property, imageService) : image;
            CurrentImage = new ImageModel(image);
            return image;
        }
        public Image PopulateEntityFB(Expression<Func<Image>> property, IImageService imageService)
        {
            var image = property.Compile().Invoke();

            if (NewImage != null && ImageCrop.ShouldCrop)
            {
                NewImage.CroppedFile = imageService.CropImage(NewImage.PostedFile.InputStream, ImageCrop);
                NewImage.CroppedContentType = "image/jpeg"; //Force to Jpeg Type
            }

            image = NewImage != null ? NewImage.PopulateEntity(property, imageService) : image;
            CurrentImage = new ImageModel(image);
            return image;
        }
    }
}