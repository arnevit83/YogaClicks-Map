using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherGalleryModel
    {
        public TeacherGalleryModel(Teacher profile, int galleryId)
        {
            Teacher = new TeacherModel(profile);
            GalleryId = galleryId;
        }

        public TeacherModel Teacher { get; private set; }

        public int GalleryId { get; private set; }
    }
}