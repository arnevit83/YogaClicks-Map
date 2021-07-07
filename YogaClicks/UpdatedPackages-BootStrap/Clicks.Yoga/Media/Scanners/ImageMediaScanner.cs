using System;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Media.Scanners
{
    public class ImageMediaScanner : IMediaResourceScanner
    {
        public ImageMediaScanner(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IImageService imageService,
            IGalleryService galleryService)
        {
            EntityContext = entityContext;
            SecurityContext = securityContext;
            ImageService = imageService;
            GalleryService = galleryService;
        }

        private IEntityContext EntityContext { get; set; }

        private ISecurityContext SecurityContext { get; set; }

        private IImageService ImageService { get; set; }

        private IGalleryService GalleryService { get; set; }

        public void Scan(MediaResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            int id;

            if (!int.TryParse(resource.Identifier, out id))
            {
                throw new UnsupportedMediaException();
            }

            resource.Image = ImageService.GetImage(id);

            if (resource.Image == null)
            {
                return;
            }

            resource.Image.Temporary = false;
        }

        public void Commit(MediaResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            if (SecurityContext.Authenticated)
            {
                var gallery = GalleryService.GetNewsfeedGallery(SecurityContext.CurrentUser.Id);

                if (gallery == null)
                {
                    gallery = GalleryService.CreateNewsfeedGallery(SecurityContext.CurrentUser.Id);
                }

                var entry = GalleryService.CreateEntry(gallery, resource.Image.Id, "");
                EntityContext.RegisterTransientEntityDependency(resource, r => r.EntityId, entry);
            }
        }
    }
}