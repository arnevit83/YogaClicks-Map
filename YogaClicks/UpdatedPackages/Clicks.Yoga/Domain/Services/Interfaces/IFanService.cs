using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IFanService
    {
        bool CanFan(User user, int entityId, string entityTypeName);
        bool IsFanned(User user, int entityId, string entityTypeName);
        int GetFanCount(int entityId, string entityTypeName);
        IList<EntityType> GetFanableTypes();
        IList<EntityHandle> GetFanned(User user);
        IList<EntityHandle> GetFanned(User user, string entityTypeName);
        IList<Profile> GetFans(int entityId, string entityTypeName); 
        void Fan(User user, int entityId, string entityTypeName);
        void Unfan(User user, int entityId, string entityTypeName);
    }
}