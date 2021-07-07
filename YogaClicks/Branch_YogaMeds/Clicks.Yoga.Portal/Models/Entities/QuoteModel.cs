using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class QuoteModel : EntityModel<Quote>
    {
        public QuoteModel(Quote entity) : base(entity) {}
        
        public string Author { get; set; }

        public string Quotation { get; set; }

        public override void Populate(Quote entity)
        {
            Id = entity.Id;
            Author = entity.Author;
            Quotation = entity.Quotation;
        }
    }
}