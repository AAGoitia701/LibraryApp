using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _dbContext;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _dbContext = db;
            Category = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
        } 

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
