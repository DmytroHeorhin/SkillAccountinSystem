using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;
using SAS.DAL.Interfaces;
using SAS.DAL.EF;
using SAS.DAL.Entities;
using System.Linq;

namespace SAS.DAL.Repositories
{
    public class BaseRepository<T> : IDisposable, IRepository<T> where T : BaseEntity
    {
        protected readonly SASContext _dbContext;
        protected readonly IDbSet<T> _dbSet;

        public BaseRepository(SASContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual T Get(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }
        
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(e => e.Id).ToList();
        }

        public virtual IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).OrderBy(e => e.Id).ToList();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
