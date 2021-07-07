using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class UserModel : EntityModel<User>, ISecurable
    {
        public UserModel(User user) : base(user) {}

        public string EmailAddress { get; private set; }

        public override void Populate(User entity)
        {
            EmailAddress = entity.EmailAddress;
        }

        public int? OwnerId
        {
            get { return Id; }
        }
    }
}