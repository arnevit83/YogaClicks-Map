using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Services.Interfaces
{
    public interface IFollowService
    {
        List<Follow> GetFollowersForCondition(int conditionId);
        bool IsConditionFollower(int followerId, string followerTypeName, int conditionId);
        Follow GetFollowerForCondition(int conditionId, int followerId, string followerTypeName);
    }
}
