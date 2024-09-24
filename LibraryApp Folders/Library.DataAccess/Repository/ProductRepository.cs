using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ProductRepository(ApplicationDBContext db) : base(db)
        {
            _dbContext = db;
        }

        public void Update(Product obj)
        {
            _dbContext.Products.Update(obj);
        }
    }
}
