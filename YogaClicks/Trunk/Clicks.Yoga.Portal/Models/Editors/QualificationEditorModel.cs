using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class QualificationEditorModel
    {
        private string _name;

        public QualificationEditorModel()
        {
        }

        public QualificationEditorModel(Qualification qualification) : this()
        {
            if (qualification == null) return;

            Id = qualification.Id;
            Name = qualification.Name;
        }

        public Qualification PopulateEntity(Qualification entity, IQualificationService qualificationService)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                if (entity != null)
                {
                    qualificationService.DeleteQualification(entity);
                    entity = null;
                }
            }
            else
            {
                if (entity == null) entity = new Qualification();
                entity.Name = Name;
            }

            return entity;
        }

        public int? Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}