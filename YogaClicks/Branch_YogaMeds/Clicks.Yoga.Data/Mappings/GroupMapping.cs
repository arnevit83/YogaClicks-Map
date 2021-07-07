using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GroupMapping : PrincipalEntityMapping<Group>
    {
        public GroupMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Description);
            HasOptional(e => e.PromoterHandle).WithMany().Map(a => a.MapKey("PromoterHandleId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
            HasMany(e => e.Styles).WithMany().Map(a => a.MapLeftKey("GroupId").MapRightKey("StyleId").ToTable("GroupStyle"));
            Property(e => e.Public).IsRequired();
            Property(e => e.Professional).IsRequired();
            Property(e => e.ProfessionsRestricted).IsRequired();
            HasMany(e => e.Professions).WithMany().Map(a => a.MapLeftKey("GroupId").MapRightKey("EntityTypeId").ToTable("GroupProfession"));
            Property(e => e.MemberInvitationsPermitted).IsRequired();
            Property(e => e.MemberPostingPermitted).IsRequired();
            Property(e => e.MemberGalleriesPermitted).IsRequired();
        }
    }
}