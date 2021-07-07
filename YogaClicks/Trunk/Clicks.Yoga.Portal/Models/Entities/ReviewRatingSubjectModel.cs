using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ReviewRatingSubjectModel : EntityModel<ReviewRatingSubject>
    {
        public ReviewRatingSubjectModel() {}

        public ReviewRatingSubjectModel(ReviewRatingSubject entity) : base(entity) {}

        public string Name { get; set; }

        public override void Populate(ReviewRatingSubject entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}