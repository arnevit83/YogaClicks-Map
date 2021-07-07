using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchTeacherInformationModel
    {
        public SearchTeacherInformationModel(Teacher teacher)
        {
            Teacher = new TeacherModel(teacher);
        }
        
        public TeacherModel Teacher { get; private set; }
    }
}