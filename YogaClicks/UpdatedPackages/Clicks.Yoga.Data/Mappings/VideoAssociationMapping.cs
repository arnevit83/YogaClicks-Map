using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class VideoAssociationMapping : EntityMapping<VideoAssociation>
    {
        public VideoAssociationMapping()
        {
            HasRequired(e => e.Video).WithMany(e => e.Associations).Map(a => a.MapKey("VideoId")).WillCascadeOnDelete(false);
            HasRequired(e => e.Handle).WithMany().Map(a => a.MapKey("HandleId")).WillCascadeOnDelete(false);
        }
    }
}