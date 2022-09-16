using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DeparterContext DbContext { get; set; }
        public RepositoryBase(DeparterContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Create(T entity)
        {
            this.DbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.DbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> GetAll()
        {
            return this.DbContext.Set<T>().AsNoTracking();
        }

        public void Update(T entity)
        {
            this.DbContext.Set<T>().Update(entity);
        }
    }
}
