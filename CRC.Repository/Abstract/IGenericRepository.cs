using System;
using System.Linq;
using System.Linq.Expressions;

namespace CRC.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        int Add(T entity);
        void Delete(T entity);
        void Edit(T entity);      
    }
}
