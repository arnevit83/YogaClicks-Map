using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Shared
{
    public class SuperUserAccessPartialModel
    {
        public SuperUserAccessPartialModel(int userId, User user)
        {
            UserId = userId;
            User = new UserModel(user);
        }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}