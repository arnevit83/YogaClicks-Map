using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Follow
{
    public class FollowButtonModel
    {
        public bool IsCurrentlyFollowing { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }
    }
}