using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> //: IGenericRepository<T> where T : class
    {
        //protected readonly RepositoryContext Context;
        //public GenericRepository(RepositoryContext context)
        //{
        //    Context = context;
        //}
        //public void Add(T entity)
        //{
        //    Context.Set<T>().Add(entity);
        //}

        //public void AddRange(List<T> entities)
        //{
        //    foreach (T entity in entities)
        //        Add(entity);
        //}

        //public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        //{
        //    return Context.Set<T>().Where(expression);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return Context.Set<T>().ToList();
        //}
        //public T GetById(int id)
        //{
        //    return Context.Set<T>().Find(id);
        //}
        //public void Remove(T entity)
        //{
        //    Context.Set<T>().Remove(entity);
        //}
    }
}
