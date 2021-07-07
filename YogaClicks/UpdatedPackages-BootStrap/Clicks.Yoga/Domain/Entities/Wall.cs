using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public abstract class Wall : Entity
    {
        protected Wall()
        {
            Posts = new List<WallPost>();
        }

        public ICollection<WallPost> Posts { get; private set; }

        public abstract Type GetWallPermissionProviderType();

        public abstract string GetWallPostNewsStoryTypeTag();

        public abstract IEntityHandle GetWallContext();
    }
}