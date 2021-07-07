using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Data.Repositories
{
    public class RepositoryQuery<TEntity> : IRepositoryQuery<TEntity> where TEntity : Entity
    {
        public RepositoryQuery(IQueryable<TEntity> queryable)
        {
            Queryable = queryable;
        }

        private IQueryable<TEntity> Queryable { get; set; } 

        public IRepositoryQuery<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            return new RepositoryQuery<TEntity>(Queryable.Include(expression));
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get { return Queryable.Expression; }
        }

        public Type ElementType
        {
            get { return Queryable.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return Queryable.Provider; }
        }
    }
}