using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]  
        public IActionResult Create(Category categoryObj)
        {
            if(categoryObj.Name == categoryObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match the Name");
            }
            if (ModelState.IsValid) //-> Chequea que todo lo que este en el modelo(category context) se cumpla
            {
                _dbContext.Categories.Add(categoryObj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Category"); //Action first -- Controller second
            }
            return View();

        }
        public IActionResult Edit(int? categoryId)
        {
            if (categoryId == 0 || categoryId == null)
            {
                return NotFound();
            }

            Category? categoryFromDB = _dbContext.Categories.Find(categoryId);
            //Category? categoryFromDb2 = _dbContext.Categories.FirstOrDefault(r => r.Id == id);
            //Category? categoryFromDb2 = _dbContext.Categories.Where(r => r.Id == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Category categoryObj)
        {
            if (ModelState.IsValid) //-> Chequea que todo lo que este en el modelo(category context) se cumpla
            {
                _dbContext.Categories.Update(categoryObj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Category"); //Action first -- Controller second
            }
            return View();

        }

        public IActionResult Delete(int? categoryId)
        {
            if (categoryId == 0 || categoryId == null)
            {
                return NotFound();
            }

            Category? categoryFromDB = _dbContext.Categories.Find(categoryId);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? categoryId)
        {
            Category? category = _dbContext.Categories.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Category");

        }


    }
}
