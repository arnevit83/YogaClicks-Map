using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    class EntityHandleInviteMapping : EntityMapping<EntityHandleInvite>
    {
        public EntityHandleInviteMapping()
        {
            HasRequired(e => e.EntityHandle)
                .WithMany()
                .Map(k => k.MapKey("EntityHandleId"))
                .WillCascadeOnDelete(true);

            HasRequired(e => e.User)
                .WithMany()
                .Map(k => k.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            Property(e => e.EmailAddress)
                .IsRequired();
        }
    }
}
