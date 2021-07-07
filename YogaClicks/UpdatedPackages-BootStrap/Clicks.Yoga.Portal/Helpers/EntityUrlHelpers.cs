using System;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Helpers
{
    public static class EntityUrlHelpers
    {
        public static string Entity(this UrlHelper helper, EntityHandleModel handle)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (handle == null)
                throw new ArgumentNullException("handle");
            if (handle.EntityType == null)
                throw new ArgumentException("Handle must specify an entity type.", "handle");
            if (handle.EntityType.Controller == null)
                throw new ArgumentException("Entity type must specify a controller.", "handle");

            return helper.Entity(handle, "Display");
        }

        public static string Entity(this UrlHelper helper, EntityHandleModel handle, string action)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (handle == null)
                throw new ArgumentNullException("handle");
            if (handle.EntityType == null)
                throw new ArgumentException("Handle must specify an entity type.", "handle");
            if (handle.EntityType.Controller == null)
                throw new ArgumentException("Entity type must specify a controller.", "handle");
            if (action == null)
                throw new ArgumentNullException("action");

            if (!handle.Active)
            {
                return helper.Action("Deleted", "Errors");
            }

            return helper.Action(action, handle.EntityType.Controller, new { Id = handle.EntityId });
        }

        public static string Entity<T>(this UrlHelper helper, PrincipalEntityModel<T> entity, string action, string controller)
            where T : PrincipalEntity
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (action == null)
                throw new ArgumentNullException("action");
            if (controller == null)
                throw new ArgumentNullException("controller");

            if (!entity.Active)
            {
                return helper.Action("Deleted", "Errors");
            }

            return helper.Action(action, controller, new { entity.Id });
        }
    }
}