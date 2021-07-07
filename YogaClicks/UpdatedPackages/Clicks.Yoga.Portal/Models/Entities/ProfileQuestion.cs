using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ProfileQuestionModel : EntityModel<ProfileQuestion>
    {
        public ProfileQuestionModel() { }

        public ProfileQuestionModel(ProfileQuestion entity) : base(entity) {}

        public string Text { get; set; }

        public int DisplayOrder { get; set; }

        public int? AnswerId { get; set; }

        public string Response { get; set; }

        public override void Populate(ProfileQuestion entity)
        {
            Id = entity.Id;
            Text = entity.Text;
            DisplayOrder = entity.DisplayOrder;
        }
    }
}