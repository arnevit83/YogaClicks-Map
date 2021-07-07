using System;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Repositories
{
    public interface IRepository
    {
        object Get(int id);
    }

    public interface IRepository<T> : IRepositoryQuery<T> where T : Entity
    {
        T Get(Expression<Func<T, bool>> expression);
        T Get(int id);
        void Add(T entity);
        void Remove(T entity);
    }
}