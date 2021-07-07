using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationReviewsModel
    {
        public OrganisationReviewsModel(TeacherTrainingOrganisation organisation, ReviewStatistics statistics)
        {
            Organisation = new TeacherTrainingOrganisationModel(organisation);
            Statistics = new ReviewStatisticsModel(statistics);
        }

        public TeacherTrainingOrganisationModel Organisation { get; private set; }

        public ReviewStatisticsModel Statistics { get; private set; }
    }
}