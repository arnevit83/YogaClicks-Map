using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpQualificationsCollectionEditorModel
    {
        public SignUpQualificationsCollectionEditorModel()
        {
            Entities = new List<QualificationEditorModel>();
            Entities.Add(new QualificationEditorModel());
        }

        public SignUpQualificationsCollectionEditorModel(IEnumerable<Qualification> collection)
        {
            Entities = new List<QualificationEditorModel>();

            foreach (var qualification in collection)
            {
                Entities.Add(new QualificationEditorModel(qualification));
            }

            if (Entities.Count == 0) Entities.Add(new QualificationEditorModel());
        }

        public IList<QualificationEditorModel> Entities { get; private set; }

        public void PopulateCollection(ICollection<Qualification> collection, IQualificationService qualificationService)
        {
            foreach (var model in Entities)
            {
                Qualification qualification;

                if (model.Id.HasValue)
                {
                    qualification = collection.FirstOrDefault(e => e.Id == model.Id);
                    model.PopulateEntity(qualification, qualificationService);
                }
                else
                {
                    qualification = model.PopulateEntity(null, qualificationService);
                    collection.Add(qualification);
                }
            }

            foreach (var qualification in collection.ToList())
            {
                if (qualification != null && !qualification.IsTransient && !Entities.Any(e => e.Id == qualification.Id))
                {
                    collection.Remove(qualification);
                    qualificationService.DeleteQualification(qualification);
                }
            }
        }
    }
}