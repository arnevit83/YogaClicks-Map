using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Fans
{
    public class FanConfirmationModel
    {
        public FanConfirmationModel(IEntityHandle entity)
        {
            if (entity != null) EntityName = entity.Name;
        }

        public string EntityName { get; set; }
    }
}