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
          //  _dbContext.Products.Include(r => r.Category);//when it retrieves product, it will come with the category table.
            //you can include multiple variables/tables
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

        //Category, Order, 
        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) 
                    {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetOne(Expression<Func<T, bool>> filter, string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }
    }
}
