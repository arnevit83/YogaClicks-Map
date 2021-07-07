using System;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Repositories
{
    public interface IRepositoryQuery<TEntity> : IQueryable<TEntity> where TEntity : Entity
    {
         IRepositoryQuery<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);
    }
}