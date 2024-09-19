
using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catRepoDb; //In order for this to work, you must register the service in Program
        public CategoryController(ICategoryRepository context)
        {
            _catRepoDb = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _catRepoDb.GetAll().ToList();
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
                _catRepoDb.Add(categoryObj);
                _catRepoDb.Save();
                TempData["success"] = "The category was created successfully";
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

            Category? categoryFromDB = _catRepoDb.GetOne(r => r.Id == categoryId);
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
                _catRepoDb.Update(categoryObj);
                _catRepoDb.Save();
                TempData["success"] = "The category was updated successfully";
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

            Category? categoryFromDB = _catRepoDb.GetOne(r => r.Id == categoryId);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? categoryId)
        {
            Category? category = _catRepoDb.GetOne(r => r.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            _catRepoDb.Delete(category);
            _catRepoDb.Save();
            TempData["success"] = "The category was deleted successfully";
            return RedirectToAction("Index", "Category");

        }


    }
}
