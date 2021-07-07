using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileFannedModel
    {
        public ProfileFannedModel(Profile profile, IEnumerable<EntityType> types, IEnumerable<EntityHandle> handles)
        {
            Profile = new ProfileModel(profile);
            EntityTypes = new Dictionary<string, EntityTypeModel>();
            EntityHandles = new Dictionary<string, IList<EntityHandleModel>>();

            foreach (var type in types)
            {
                EntityTypes.Add(type.Name, new EntityTypeModel(type));
            }

            foreach (var handle in handles)
            {
                var type = handle.EntityType;
                IList<EntityHandleModel> list;

                if (EntityHandles.ContainsKey(type.Name))
                {
                    list = EntityHandles[type.Name];
                }
                else
                {
                    list = new List<EntityHandleModel>();
                    EntityHandles[type.Name] = list;
                    EntityTypes[type.Name] = new EntityTypeModel(type);
                }

                list.Add(new EntityHandleModel(handle));
            }
        }

        public ProfileModel Profile { get; private set; }

        public IDictionary<string, EntityTypeModel> EntityTypes { get; private set; } 

        public IDictionary<string, IList<EntityHandleModel>> EntityHandles { get; private set; }
    }
}