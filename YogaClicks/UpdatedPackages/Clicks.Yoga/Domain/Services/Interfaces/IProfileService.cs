using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IProfileService
    {
        bool ProfileVisibleToCurrentUser(int profileId);
        Gender GetGender(int id);
        Profile GetProfile(int id);
        Profile GetProfileForDisplay(int id);
        Profile GetProfileForEditing(int id);
        Profile GetProfile(PrincipalEntity entity);
        Profile GetProfile(ISecurable securable);
        IList<EntityType> GetProfessionalEntityTypes();
        IList<EntityHandle> GetProfessionalEntities();
        IList<EntityHandle> GetProfessionalEntitiesAndProfile();
        IList<EntityHandle> GetActors();
        IList<EntityHandle> GetGalleryAssociateHandles();
        IList<Group> GetGroups(int userId);
        IList<Image> GetPromotedGalleryImages(int userId);
        IList<Profile> GetFans();
        IList<ProfileQuestion> GetProfileQuestions();
        IList<ProfileAnswer> GetProfileAnswersForCurrentUser();
        void AddProfile(Profile profile);
        void DeleteOwned(int id, string typeName);
        void UpdateProfileQuestions(Profile profile, IList<ProfileAnswer> answers);
        void Claim(int entityId, string entityTypeName);
        void Unclaim(int entityId, string entityTypeName);
        void Hide(Profile profile);
    }
}