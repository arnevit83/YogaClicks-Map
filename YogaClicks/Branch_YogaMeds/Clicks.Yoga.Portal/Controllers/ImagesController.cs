using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Controllers
{
    public class ImagesController : YogaController
    {
        public ImagesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IImageService imageService)
            : base(workUnit, securityContext)
        {
            ImageService = imageService;
        }

        private IImageService ImageService { get; set; }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Upload(ImageUploadModel model)
        {
            var type = ImageService.GetImageType(model.PostedFile.ContentType);

            if (type == null) return Json(new { Succeeded = false });

            var image = new Image(model.PostedFile.InputStream, type)
            {
                Temporary = true,
                Owner = SecurityContext.CurrentUser
            };

            ImageService.AddImage(image);
            WorkUnit.Commit();

            return Json(new ImageModel(image));
        }
    }
}