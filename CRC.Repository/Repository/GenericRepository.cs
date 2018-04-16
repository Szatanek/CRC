using CRC.Repository.Abstract;
using CRC.Repository.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CRC.Repository.Repository
{
    public class GenericRepository<T> :
     IGenericRepository<T> where T : Entity
    {
        private Context.AppContext _context;

        public GenericRepository(Context.AppContext context)
        {
            _context = context;
        }

        public Context.AppContext Context
        {

            get { return _context; }
            set { _context = value; }
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public T GetById(int id)
        {
           
            return _context.Set<T>().SingleOrDefault(item => item.Id == id);
        }

        public virtual int Add(T entity)
        {
            if (GetAll().Any())
            {
                entity.Id = GetAll().Max(u => u.Id) + 1;
            }
            else
            {
                entity.Id = 1;
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }       
    }
}
