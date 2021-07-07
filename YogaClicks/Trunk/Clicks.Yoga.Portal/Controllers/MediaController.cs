using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Media;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Media;

namespace Clicks.Yoga.Portal.Controllers
{
    public class MediaController : YogaController
    {
        public MediaController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IImageService imageService,
            IMediaService mediaService)
            : base(workUnit, securityContext)
        {
            ImageService = imageService;
            MediaService = mediaService;
        }

        private IImageService ImageService { get; set; }

        private IMediaService MediaService { get; set; }

        public ActionResult PostPartial(IEnumerable<int> ids, int? width)
        {
            if (ids == null)
            {
                return new EmptyResult();
            }

            var resources = MediaService.GetResources(ids);
            var model = new MediaPostPartialModel(resources, width);

            WorkUnit.Commit();

            return PartialView(model);
        }

        public ActionResult PreviewPartial(string uri)
        {
            if (uri == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
            }

            try
            {
                var resource = MediaService.ScanResource(uri);
                var model = new MediaPreviewPartialModel(resource);

                WorkUnit.Commit();

                return PartialView(model);
            }
            catch (UnsupportedMediaException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
            }
        }

        public ActionResult ScanImages(string uri)
        {
            if (uri == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
            }

            try
            {
                var resource = MediaService.ScanWebsiteImages(uri);
                var model = new ImageModel(resource.Image);

                WorkUnit.Commit();

                return PartialView("ImagePartial", model);
            }
            catch (UnsupportedMediaException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
            }
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UploadImage(ImageUploadModel model)
        {
            var type = ImageService.GetImageType(model.PostedFile.ContentType);

            if (type == null) return Json(new { Succeeded = false });

            var image = ImageService.CreateTemporaryImage(
                typeof(GalleryEntry).GetProperty("Image"),
                model.PostedFile.InputStream,
                model.PostedFile.ContentType);

            WorkUnit.Commit();

            var uri = string.Format("urn:yogaclicks:entity:Image:{0}", image.Id);

            return Content(Url.Action("PreviewPartial", new { Uri = uri }));
        }
    }
}