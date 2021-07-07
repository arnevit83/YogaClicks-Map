using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class StudyModel : EntityModel<Study>
    {
        public StudyModel() { }

        public StudyModel(Study entity) : base(entity) { }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public string DateOfStudy { get; set; }

        public string Journal { get; set; }

        public string Volume { get; set; }

        public string Institution { get; set; }

        public string Source { get; set; }

        public int NumberOfCitations { get; set; }

        public List<AuthorModel> Authors { get; set; }

        public List<TherapyTypeModel> TherapyTypes { get; set; }

        public override void Populate(Study entity)
        {
            Id = entity.Id;
            Title = entity.Title.Replace(Environment.NewLine, string.Empty);
            Abstract = entity.Abstract;
            DateOfStudy = entity.DateOfStudy;
            Journal = entity.Journal;
            Volume = entity.Volume;
            Institution = entity.Institution;
            Source = entity.Source;
            NumberOfCitations = entity.NumberOfCitations;

            var authors = new List<AuthorModel>();
            var therapyTypes = new List<TherapyTypeModel>();

            foreach (var author in entity.Authors)
            {
                authors.Add(new AuthorModel(author));
            }

            foreach (var therapyType in entity.TherapyTypes)
            {
                therapyTypes.Add(new TherapyTypeModel(therapyType));
            }

            Authors = authors;
            TherapyTypes = therapyTypes;
        }
    }
}