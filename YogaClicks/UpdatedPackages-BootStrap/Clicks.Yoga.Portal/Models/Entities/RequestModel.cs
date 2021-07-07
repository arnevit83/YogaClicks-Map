using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class RequestModel : EntityModel<Request>
    {
        public RequestModel(Request entity) : base(entity) {}

        public RequestTypeModel Type { get; set; }

        public string Message { get; set; }

        public EntityHandleModel ActorHandle { get; set; }

        public override void Populate(Request entity)
        {
            Type = new RequestTypeModel(entity.Type);
            Message = entity.GetMessage();
            ActorHandle = new EntityHandleModel(entity.ActorHandle);
        }
    }
}