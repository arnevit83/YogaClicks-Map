using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountDeactivateModel
    {
        public AccountDeactivateModel()
        {
            Strategy = DeactivationStrategy.Unclaim;
        }

        public AccountDeactivateModel(User user) : this()
        {
            ProfileId = user.Profile.Id;
        }

        public int ProfileId { get; set; }
        public DeactivationStrategy Strategy { get; set; }
    }
}