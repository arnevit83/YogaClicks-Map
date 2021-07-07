using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class StudyAuthorsUpdateModel
    {
        public StudyAuthorsUpdateModel() { }

        public StudyAuthorsUpdateModel(Study study)
        {
            Id = study.Id;

            var authorsList = new List<AuthorModel>();

            foreach (var author in study.Authors)
            {
                authorsList.Add(new AuthorModel(author));
            }

            Authors = authorsList;
        }

        public int Id { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}