using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class AuthorModel : EntityModel<Author>
    {
        public AuthorModel() { }

        public AuthorModel(Author entity) : base(entity) { }

        public string Name { get; set; }

        public override void Populate(Author entity)
        {
            Id = entity.Id;
            Name = entity.Name.Replace(Environment.NewLine, string.Empty);
        }
    }
}