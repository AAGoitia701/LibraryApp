using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<T> dbSet; //dummy to later convert it to categories table
        public Repository(ApplicationDBContext db)
        {   
            _dbContext = db;
            this.dbSet = _dbContext.Set<T>(); //We set the dbSet as type T, so when we change the Y to Category dbSet changes to category too
            // _dbContext.Categories == dbSet
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
    }
}
