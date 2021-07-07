using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class StudyDetailsUpdateModel
    {
        public StudyDetailsUpdateModel () {}

        public StudyDetailsUpdateModel(StudyModel study) 
        {
            Id = study.Id;
            Title = study.Title;
            Abstract = study.Abstract;
            Citations = study.NumberOfCitations;
            DateOfStudy = study.DateOfStudy;
            Journal = study.Journal;
            Volume = study.Volume;
            Institution = study.Institution;
            Source = study.Source;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public int Citations { get; set; }

        public string DateOfStudy { get; set; }

        public string Journal { get; set; }

        public string Volume { get; set; }

        public string Institution { get; set; }

        public string Source { get; set; }

        public void PopulateEntity(
            Study study)
        {
            study.Title = Title;
            study.Abstract = Abstract;
            study.NumberOfCitations = Citations;
            study.DateOfStudy = DateOfStudy;
            study.Journal = Journal;
            study.Volume = Volume;
            study.Institution = Institution;
            study.Source = Source;
        }
    }
}