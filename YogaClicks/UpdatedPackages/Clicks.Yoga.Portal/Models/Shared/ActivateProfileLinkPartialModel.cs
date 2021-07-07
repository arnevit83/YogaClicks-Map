using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Shared
{
    public class ActivateProfileLinkPartialModel
    {
        public ActivateProfileLinkPartialModel(int entityId, string entityTypeName)
        {
            EntityId = entityId;
            EntityTypeName = entityTypeName;
        }

        public int EntityId { get; set; }

        public string EntityTypeName { get; set; }
    }
}