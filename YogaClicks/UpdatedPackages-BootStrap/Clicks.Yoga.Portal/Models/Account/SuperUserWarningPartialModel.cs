using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Account
{
    public class SuperUserWarningPartialModel
    {
        public SuperUserWarningPartialModel(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}