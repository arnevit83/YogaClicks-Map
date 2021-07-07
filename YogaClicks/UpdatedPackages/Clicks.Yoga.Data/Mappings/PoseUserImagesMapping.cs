using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class PoseUserImagesMapping : EntityMapping<PoseUserImages>
    {
        public PoseUserImagesMapping()
        {
            HasRequired(e => e.Pose).WithMany(e => e.UserImagesPoses).Map(a => a.MapKey("PoseId"));
   
            HasRequired(e => e.Image).WithMany().Map(a => a.MapKey("ImageId")).WillCascadeOnDelete(false);
          
        }
    }
}