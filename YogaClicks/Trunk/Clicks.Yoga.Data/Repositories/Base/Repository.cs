using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Data.Repositories
{
    public class Repository : IRepository
    {
        public Repository(YogaDataContext context, Type entityType)
        {
            Set = context.Set(entityType);
        }

        protected DbSet Set { get; private set; }

        public object Get(int id)
        {
            var entity = Set.Find(id);
            var principal = entity as PrincipalEntity;

            if (principal != null && !principal.Active) return null;

            return entity;
        }
    }

    public class Repository<T> : IRepository<T> where T : Entity
    {
        public Repository(YogaDataContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        private YogaDataContext Context { get; set; }

        protected DbSet<T> Set { get; private set; }

        public virtual IQueryable<T> Queryable
        {
            get { return Set; }
        }

        public IRepositoryQuery<T> Include<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return new RepositoryQuery<T>(Queryable.Include(expression));
        }

        public virtual IQueryable<T> WithRelated
        {
            get { return Queryable; }
        }

        public IQueryable<T> WithoutRelated
        {
            get { return Queryable; }
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return WithoutRelated.Where(expression).FirstOrDefault();
        }

        public virtual T Get(int id)
        {
            return WithoutRelated.FirstOrDefault(e => e.Id == id);
        }

        public virtual void Add(T entity)
        {
            Set.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            Context.RaiseDeletingEvent(entity);
            Set.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return WithRelated.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return WithRelated.GetEnumerator();
        }

        public Type ElementType
        {
            get { return WithRelated.ElementType; }
        }

        public Expression Expression
        {
            get { return WithRelated.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return WithRelated.Provider; }
        }
    }
}