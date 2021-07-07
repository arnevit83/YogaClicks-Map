using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class TtoQualificationsCollectionEditorModel
    {
        public TtoQualificationsCollectionEditorModel()
        {
            Entities = new List<TtoQualificationEditorModel>();
            Entities.Add(new TtoQualificationEditorModel());
        }

        public TtoQualificationsCollectionEditorModel(IEnumerable<TeacherTrainingOrganisationQualification> collection)
        {
            Entities = new List<TtoQualificationEditorModel>();

            foreach (var qualification in collection)
            {
                Entities.Add(new TtoQualificationEditorModel(qualification));
            }

            if (Entities.Count == 0) Entities.Add(new TtoQualificationEditorModel());
        }

        public IList<TtoQualificationEditorModel> Entities { get; private set; }

        public void PopulateCollection(ICollection<TeacherTrainingOrganisationQualification> collection, IQualificationService qualificationService)
        {
            foreach (var model in Entities)
            {
                TeacherTrainingOrganisationQualification qualification;

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
                    qualificationService.DeleteTtoQualification(qualification);
                }
            }
        }
    }
}