using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SAS.DAL.Entities;

namespace SAS.DAL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T entity);
        void Delete(int id);
        void Dispose();
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        void Update(T entity);
    }
}