using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpSchoolCollectionEditorModel
    {
        public SignUpSchoolCollectionEditorModel()
        {
            Entities = new List<SchoolEditorModel>();
            Entities.Add(new SchoolEditorModel());
        }

        public SignUpSchoolCollectionEditorModel(IEnumerable<School> collection)
        {
            Entities = new List<SchoolEditorModel>();

            foreach (var school in collection)
            {
                Entities.Add(new SchoolEditorModel(school));
            }

            if (Entities.Count == 0) Entities.Add(new SchoolEditorModel());
        }

        public IList<SchoolEditorModel> Entities { get; private set; }

        public void PopulateCollection(ICollection<School> collection, ISchoolService schoolService)
        {
            foreach (var model in Entities)
            {
                School school;

                if (model.Id.HasValue)
                {
                    school = collection.FirstOrDefault(e => e.Id == model.Id);
                    model.PopulateEntity(school, schoolService);
                }
                else
                {
                    school = model.PopulateEntity(null, schoolService);
                    collection.Add(school);
                }
            }

            foreach (var school in collection.ToList())
            {
                if (school != null && !school.IsTransient && !Entities.Any(e => e.Id == school.Id))
                {
                    collection.Remove(school);
                    schoolService.DeleteSchool(school);
                }
            }
        }
    }
}