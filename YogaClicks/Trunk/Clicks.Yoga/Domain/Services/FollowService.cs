using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Services
{
    public class FollowService : ServiceBase, IFollowService
    {
        public FollowService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) { }

        public List<Follow> GetFollowersForCondition(int conditionId)
        {
            return EntityContext.Followers
                .Where(x => x.Conditions.Select(y => y.Id).Contains(conditionId))
               .ToList();
        }

        public Follow GetFollowerForCondition(int conditionId, int followerId, string followerTypeName)
        {
            return EntityContext.Followers
                .Where(x => x.FollowerId == followerId)
                .Where(x => x.FollowerType.Name == followerTypeName)
                .Where(x => x.Conditions.Select(y => y.Id).Contains(conditionId))
                .FirstOrDefault();
        }

        public bool IsConditionFollower(int followerId, string followerTypeName, int conditionId)
        {
            return EntityContext.Followers
                .Where(x => x.FollowerId == followerId)
                .Where(x => x.FollowerType.Name == followerTypeName)
                .Where(x => x.Conditions.Select(y => y.Id).Contains(conditionId))
                .Any();
        }
    }
}
