using System;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Data.Repositories
{
    public class ProfileRepository : PrincipalRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(YogaDataContext context) : base(context) {}

        public Profile GetIgnoringActive(int id)
        {
            return Set.FirstOrDefault(e => e.Id == id);
        }

        public Profile GetIgnoringActive(Expression<Func<Profile, bool>> expression)
        {
            return Set.FirstOrDefault(expression);
        }
    }
}