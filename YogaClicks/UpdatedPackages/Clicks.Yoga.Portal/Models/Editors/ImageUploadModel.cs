using System;
using System.IO;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Common;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class ImageUploadModel
    {
        public Image PopulateEntity(Expression<Func<Image>> property, IImageService imageService)
        {
            var image = property.Compile().Invoke();

            if (image != null && (Exists || Incoming)) image.Active = false;

            if (Exists)
            {
                image = imageService.ReplaceImage(image, Id.Value);
            }
            else if (Incoming)
            {
                image = imageService.ReplaceImage(ReflectionFunctions.GetPropertyInfo(property), image, ImageStream(), ContentType());
            }

            return image;
        }

        public int? Id { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }

        public Stream CroppedFile { get; set; }

        public string CroppedContentType { get; set; }

        private string ContentType()
        {
            return CroppedContentType ?? PostedFile.ContentType; 
        }

        private Stream ImageStream()
        {
         return CroppedFile ?? PostedFile.InputStream;   
        }

        public bool Exists
        {
            get { return Id != null; }
        }

        public bool Incoming
        {
            get { return PostedFile != null;  }
        }
    }
}