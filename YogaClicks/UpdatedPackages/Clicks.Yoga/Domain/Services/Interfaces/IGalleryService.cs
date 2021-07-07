using System.Collections.Generic;
using System.IO;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IGalleryService
    {
        bool GalleryVisible(Gallery gallery);
        bool EntryPromoted(GalleryEntry entry);
        IList<Gallery> GetAssociatedGalleriesForDisplay(int entityId, string entityTypeName);
        IList<GalleryEntry> GetEntries(int galleryId);
        Gallery GetNewsfeedGallery(int ownerId);
        Gallery GetGallery(int id);
        Gallery GetGalleryForDisplay(int id);
        Gallery GetGalleryForEditing(int id);
        GalleryEntry GetEntryForDisplay(int id);
        GalleryEntry GetEntryForEditing(int id);
        GalleryPermissions GetPermissions(int entityId, string entityTypeName);
        Gallery CreateGallery(int ownerId);
        Gallery CreateNewsfeedGallery(int ownerId);
        GalleryEntry CreateEntry(Gallery gallery, int imageId, string name);
        GalleryEntry CreateEntry(Gallery gallery, Stream stream, string contentType, string name);
        void AssociateGallery(Gallery gallery, int handleId);
        void DisassociateGallery(Gallery gallery, int handleId);
        void PromoteEntry(GalleryEntry entry);
        void UnpromoteEntry(GalleryEntry entry);
        void DeleteGallery(int id);
        void DeleteEntry(int id);
    }
}