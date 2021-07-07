using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services.Interfaces
{
    public interface ISharingService
    {
        void ShareEntityByEmail(EntityReference entity, string emailAddress, string message);

        void ShareConditionByEmail(EntityReference reference, string emailAddress, string message);
    }
}