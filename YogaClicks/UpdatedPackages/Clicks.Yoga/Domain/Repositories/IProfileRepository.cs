using System;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Profile GetIgnoringActive(int id);
        Profile GetIgnoringActive(Expression<Func<Profile, bool>> expression);
    }
}