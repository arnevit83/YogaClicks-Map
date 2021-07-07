using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Galleries;
using Clicks.Yoga.Portal.Models.Shared;
using System.Collections.Generic;

namespace Clicks.Yoga.Portal.Controllers
{
    public class GalleriesController : YogaController
    {
        public GalleriesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IEntityService entityService,
            IGalleryService galleryService,
            IImageService imageService,
            IProfileService profileService,
            IVideoService videoService)
            : base(workUnit, securityContext)
        {
            EntityService = entityService;
            GalleryService = galleryService;
            ImageService = imageService;
            ProfileService = profileService;
            VideoService = videoService;
        }

        private IEntityService EntityService { get; set; }

        private IGalleryService GalleryService { get; set; }

        private IImageService ImageService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IVideoService VideoService { get; set; }

        
        public ActionResult DisplayPartial(int id, int entityId, string entityTypeName)
        {
            var entityType = EntityService.GetEntityType(entityTypeName);
            var gallery = GalleryService.GetGalleryForDisplay(id);
            var entries = GalleryService.GetEntries(id);
            var associateOptions = new List<EntityHandle>();

            if (SecurityContext.Authenticated)
            {
                associateOptions = ProfileService.GetGalleryAssociateHandles().ToList();    
            }

            return View(new GalleryDisplayPartialModel(gallery, entries, associateOptions, entityId, entityType));
        }

        
        public ActionResult EntryThumbnailPartial(int entryId)
        {
            var entry = GalleryService.GetEntryForDisplay(entryId);
            return PartialView(new GalleryEntryThumbnailPartialModel(entry));
        }

        
        public ActionResult UploadThumbnailPartial(int imageId)
        {
            var image = ImageService.GetImage(imageId);

            if (!SecurityContext.CanUpdate(image.Owner))
                throw new PermissionDeniedException();

            return View(new GalleryUploadThumbnailPartialModel(image));
        }

        
        public ActionResult EntryPopup(int entryId)
        {
            var entry = GalleryService.GetEntryForDisplay(entryId);
            return View(new GalleryEntryPopupModel(entry));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult AssociatedPartial(int entityId, string entityTypeName)
        {
            var entityType = EntityService.GetEntityType(entityTypeName);
            var galleries = GalleryService.GetAssociatedGalleriesForDisplay(entityId, entityTypeName);
            var permissions = GalleryService.GetPermissions(entityId, entityTypeName);

            return View("AssociatedPartial", new GalleryAssociatedPartialModel(entityId, entityType, galleries, permissions));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult AssociatedVideosPartial(int entityId, string entityTypeName)
        {
            var entityType = EntityService.GetEntityType(entityTypeName);
            var videos = VideoService.GetAssociatedVideos(entityId, entityTypeName);
            var permissions = GalleryService.GetPermissions(entityId, entityTypeName);

            var model = new GalleryAssociatedVideosPartialModel(entityId, entityType, videos, permissions, videos.Count, 1, 5);

            return View("AssociatedVideosPartial", model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult VideoPartial(int entityId, string entityTypeName, int videoId)
        {
            var video = VideoService.GetVideo(videoId);
            var associateHandles = new List<EntityHandle>();
            
            if (SecurityContext.Authenticated)
            {
                 associateHandles = ProfileService.GetGalleryAssociateHandles().ToList();
            }

            var model = new GalleryVideoPartialModel(
                entityId,
                entityTypeName,
                video,
                GalleryService.GetPermissions(entityId, entityTypeName),
                VideoService.GetAbilityLevels(),
                associateHandles
               );

            return View(model);
        }

        [HttpPost]
        public ActionResult FilteredVideosPartial(int entityId, string entityTypeName, bool? isClass, string length, int? level, int page)
        {
            var entityType = EntityService.GetEntityType(entityTypeName);
            var videos = VideoService.GetAssociatedVideos(entityId, entityTypeName);
            var permissions = GalleryService.GetPermissions(entityId, entityTypeName);

            if (isClass.HasValue && isClass.Value)
            {
                videos = videos.Where(e => e.IsClass && (e.Length == length || string.IsNullOrEmpty(length) && (!level.HasValue || level.Value == e.AbilityLevel.Id))).ToList();
            }

            var model = new GalleryAssociatedVideosPartialModel(entityId, entityType, videos, permissions, videos.Count, page, 5);

            return View(model);
        }

        
        [YogaAuthorize]
        public ActionResult Create(int entityId, string entityTypeName)
        {
            var associateOptions = ProfileService.GetGalleryAssociateHandles();
            return View(new GalleryCreateModel(entityId, entityTypeName, associateOptions));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Create(GalleryCreateModel model)
        {
            var gallery = GalleryService.CreateGallery(SecurityContext.CurrentUser.Id);

            model.PopulateEntity(gallery, GalleryService, EntityService);

            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(null, "CreateGallery"));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVideo(string url, int entityId, string entityTypeName)
        {
            Video video = null;

            try
            {
                video = VideoService.CreateVideo(url, entityId, entityTypeName);
                WorkUnit.Commit();
            }
            catch (ArgumentException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            var model = new GalleryVideoPartialModel(
                entityId,
                entityTypeName,
                video,
                new GalleryPermissions(),
                VideoService.GetAbilityLevels(),
                ProfileService.GetGalleryAssociateHandles());

            return View("VideoPartial", model);
        }

        public ActionResult CreateVideoAjax(string url)
        {
            try
            {
                var video = VideoService.CreateVideo(url);
                WorkUnit.Commit();
                return Json(new VideoModel(video));
            }
            catch (ArgumentException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
            }
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(GalleryUpdateModel model)
        {
            var gallery = GalleryService.GetGalleryForEditing(model.Id);

            model.PopulateEntity(gallery, GalleryService);

            WorkUnit.Commit();

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateEntry(GalleryUpdateEntryModel model)
        {
            var entry = GalleryService.GetEntryForEditing(model.EntryId);

            if (model.Name != null)
            {
                entry.Name = model.Name != null ? model.Name.Substring(0, Math.Min(model.Name.Length, 100)) : "";
            }

            if (model.IsPromoted.HasValue)
            {
                if (model.IsPromoted.Value && !entry.Promoted)
                {
                    GalleryService.PromoteEntry(entry);
                }
                else if (!model.IsPromoted.Value && entry.Promoted)
                {
                    GalleryService.UnpromoteEntry(entry);
                }
            }

            if (model.IsCover.HasValue && model.IsCover.Value)
            {
                entry.Gallery.Cover = entry.Image;
            }

            WorkUnit.Commit();

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateVideo(GalleryUpdateVideoModel model)
        {
            var video = VideoService.GetVideo(model.Id);

            if (!SecurityContext.CanUpdate(video))
                throw new PermissionDeniedException();

            model.PopulateEntity(video, VideoService);

            if (model.AbilityLevelId == 0)
            {
                video.AbilityLevel = null;
            }

            WorkUnit.Commit();

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Disassociate(int id, int entityId, string entityTypeName)
        {
            var gallery = GalleryService.GetGalleryForDisplay(id);
            var handle = EntityService.GetEntityHandle(entityId, entityTypeName);

            if (handle != null)
            {
                GalleryService.DisassociateGallery(gallery, handle.Id);
                WorkUnit.Commit();
            }

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult DisassociateVideo(int videoId, int entityId, string entityTypeName)
        {
            var video = VideoService.GetVideo(videoId);
            var handle = EntityService.GetEntityHandle(entityId, entityTypeName);

            if (handle != null)
            {
                VideoService.DisassociateVideo(video, handle.Id);
                WorkUnit.Commit();
            }

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult DeleteEntry(int targetId)
        {
            GalleryService.DeleteEntry(targetId);
            WorkUnit.Commit();

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult DeleteImage(int targetId)
        {
            ImageService.DeleteImage(targetId);
            WorkUnit.Commit();

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UploadEntry(int id, ImageUploadModel model)
        {
            var gallery = GalleryService.GetGallery(id);
            var entry = GalleryService.CreateEntry(gallery, model.PostedFile.InputStream, model.PostedFile.ContentType, "");

            WorkUnit.Commit();

            return Content(Url.Action("EntryThumbnailPartial", new { EntryId = entry.Id }));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UploadImage(ImageUploadModel model)
        {
            var property = typeof(GalleryEntry).GetProperty("Image");
            var image = ImageService.CreateTemporaryImage(property, model.PostedFile.InputStream, model.PostedFile.ContentType);

            WorkUnit.Commit();

            return Content(Url.Action("UploadThumbnailPartial", new { ImageId = image.Id }));
        }

        public ActionResult UpdatedConfirmation()
        {
            return View();
        }
    }
}