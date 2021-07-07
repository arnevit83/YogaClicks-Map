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
    public class QualificationService : ServiceBase, IQualificationService
    {
        public QualificationService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) { }

        public void DeleteQualification(Qualification qualification)
        {
            EntityContext.Qualifications.Remove(qualification);
        }

        public void DeleteTtoQualification(TeacherTrainingOrganisationQualification qualification)
        {
            EntityContext.TeacherTrainingOrganisationsQualifications.Remove(qualification);
        }
    }
}
