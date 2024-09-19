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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {//So you don't have to add all of the methods(including from IRepo), you add Repository as an implementation with the ICatRepo
     //dbContext will be provided when the pbject is created, but it gives an error in this class, so we fix that:

        private ApplicationDBContext _dbContext;

        public CategoryRepository(ApplicationDBContext db) : base(db) //-> base, so we can get the same dbContext that is created in Repository. Not creating a new one.
        {
            _dbContext = db;
        }

        void ICategoryRepository.Update(Category obj)
        {
            _dbContext.Categories.Update(obj);
        }
    }
}
