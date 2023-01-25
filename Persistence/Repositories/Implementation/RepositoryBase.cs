using Domain.Entities;
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

        public virtual void Update(T entity)
        {
            this.DbContext.Set<T>().Update(entity);
        }

        public void CreateMultiple(List<T> entities)
        {
            this.DbContext.Set<T>().AddRange(entities);
        }

        public void DeleteMultiple(List<T> entities)
        {
            this.DbContext.Set<T>().RemoveRange(entities);
        }

        public void UpdateMultiple(List<T> entity)
        {
            this.DbContext.Set<T>().UpdateRange(entity);
        }
    }

    public class EntityRepositoryBase<T> : RepositoryBase<T>, IEntityRepositoryBase<T> where T : EntityBase
    {
        public EntityRepositoryBase(DeparterContext dbContext) : base(dbContext)
        {
        }

        public override void Update(T entity)
        {
            entity.DataModificacao = DateTime.Now;
            this.DbContext.Set<T>().Update(entity);
        }

        public IQueryable<T> FindById(Guid id)
        {
            return this.DbContext.Set<T>().Where(x => x.Id == id).AsNoTracking();
        }

        public void DeleteById(Guid id)
        {
            var entity = this.DbContext.Set<T>().Where(x => x.Id == id).AsNoTracking().FirstOrDefault();

            if (entity is null)
            {
                return;
            }

            this.DbContext.Set<T>().Remove(entity);
        }
    }
}
