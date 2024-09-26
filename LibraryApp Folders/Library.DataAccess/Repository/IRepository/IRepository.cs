using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //For exmaple -> T ==Category
        //Get the complete list of categories
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetOne(Expression<Func<T, bool>> filter, string? includeProperties);
        void Add(T entity); 
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
