using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class DeleteButtonModel
    {
        public DeleteButtonModel(int entityId, EntityType entityType, Profile profile)
        {
            EntityId = entityId;
            EntityType = new EntityTypeModel(entityType);
            Profile = new ProfileModel(profile);
        }

        public int EntityId { get; set; }

        public EntityTypeModel EntityType { get; private set; }

        public ProfileModel Profile { get; private set; }
    }
}