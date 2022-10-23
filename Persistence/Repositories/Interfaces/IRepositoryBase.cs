using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public interface IEntityRepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        IQueryable<T> FindById(Guid id);
        void DeleteById(Guid id);
    }   
}
