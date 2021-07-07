namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityAttendanceActorPickerModel
    {
        public ActivityAttendanceActorPickerModel() {}

        public ActivityAttendanceActorPickerModel(int actorId, string actorTypeName)
        {
            ActorId = actorId;
            ActorTypeName = actorTypeName;
        }

        public int ActorId { get; set; }

        public string ActorTypeName { get; set; }
    }
}