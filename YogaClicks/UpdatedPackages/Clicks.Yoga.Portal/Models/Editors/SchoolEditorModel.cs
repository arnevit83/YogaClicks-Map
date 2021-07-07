using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SchoolEditorModel
    {
        private string _name;

        public SchoolEditorModel()
        {
        }

        public SchoolEditorModel(School school) : this()
        {
            if (school == null) return;

            Id = school.Id;
            Name = school.Name;
        }

        public School PopulateEntity(School entity, ISchoolService schoolService)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                if (entity != null)
                {
                    schoolService.DeleteSchool(entity);
                    entity = null;
                }
            }
            else
            {
                if (entity == null) entity = new School();
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