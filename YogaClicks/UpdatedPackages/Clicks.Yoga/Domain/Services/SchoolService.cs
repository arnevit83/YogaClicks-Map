using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Services
{
    public class SchoolService : ServiceBase, ISchoolService
    {
        public SchoolService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) { }
        
        public void DeleteSchool(School school)
        {
            EntityContext.Schools.Remove(school);
        }
    }
}
