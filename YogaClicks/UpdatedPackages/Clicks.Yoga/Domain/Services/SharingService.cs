using System;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Emails;

namespace Clicks.Yoga.Domain.Services
{
    public class SharingService : ServiceBase, ISharingService
    {
        public SharingService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IMedicService medicService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            MedicService = medicService;
        }

        private IEntityService EntityService { get; set; }
        private IMedicService MedicService { get; set; }

        public void ShareEntityByEmail(EntityReference reference, string emailAddress, string message)
        {
            if (emailAddress == null)
                throw new ArgumentNullException("emailAddress");

            var handle = EntityService.EnsureEntityHandle(reference);

            if (handle == null)
                throw new ArgumentOutOfRangeException("reference");

            new ShareEntityEmail(SecurityContext.CurrentActor, handle, emailAddress, message).Send();
        }

        public void ShareConditionByEmail(EntityReference reference, string emailAddress, string message)
        {
            if (emailAddress == null)
                throw new ArgumentNullException("emailAddress");

            var condition  = MedicService.GetCondition(reference.EntityId);

            var handle = new EntityHandle
            {
                Active = true,
                EntityId = reference.EntityId,
                EntityType = EntityService.GetEntityType(reference.EntityTypeName),
                Name = condition.Name
            };

            if (handle == null)
                throw new ArgumentOutOfRangeException("reference");

            new ShareEntityEmail(SecurityContext.CurrentActor, handle, emailAddress, message).Send();
        }
    }
}