using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ProfileAnswerModel : EntityModel<ProfileAnswer>
    {
        public ProfileAnswerModel(ProfileAnswer entity) : base(entity) {}

        public ProfileQuestionModel Question { get; set; }

        public string Text { get; set; }

        public override void Populate(ProfileAnswer entity)
        {
            Question = new ProfileQuestionModel(entity.Question);
            Text = entity.Text;
        }
    }
}