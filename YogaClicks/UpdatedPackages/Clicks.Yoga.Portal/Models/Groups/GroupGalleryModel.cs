using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupGalleryModel
    {
        public GroupGalleryModel(Group profile, GroupPermissions permissions, int galleryId)
        {
            Group = new GroupModel(profile);
            Permissions = permissions;
            GalleryId = galleryId;
        }

        public GroupModel Group { get; private set; }

        public GroupPermissions Permissions { get; private set; }

        public int GalleryId { get; private set; }
    }
}