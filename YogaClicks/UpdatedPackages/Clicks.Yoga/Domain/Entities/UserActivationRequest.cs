using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class UserActivationRequest : Entity
    {
        public UserActivationRequest()
        {
            Key = Guid.NewGuid().ToString();
        }

        public virtual User User { get; set; }

        public string Key { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletionTime { get; set; }

        public void ActivateUser()
        {
            if (Completed) throw new RepeatedActionException();

            User.Active = true;
            User.Profile.Active = !User.Profile.Professional;

            Completed = true;
            CompletionTime = DateTime.Now;
        }
    }
}