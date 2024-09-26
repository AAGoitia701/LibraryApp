
using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //we replace icategoryrepo with Iunit of works. IUnitOfWorks internally has ICategoryRepo
        //private readonly ICategoryRepository _catRepoDb; //In order for this to work, you must register the service in Program
        private readonly IUnitOfWork _unitOWDb;
        public CategoryController(IUnitOfWork context)
        {
            _unitOWDb = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOWDb.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category categoryObj)
        {
            if (categoryObj.Name == categoryObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match the Name");
            }
            if (ModelState.IsValid) //-> Chequea que todo lo que este en el modelo(category context) se cumpla
            {
                _unitOWDb.Category.Add(categoryObj);
                _unitOWDb.Save();
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

            Category? categoryFromDB = _unitOWDb.Category.GetOne(r => r.Id == categoryId, includeProperties: "Category");
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
                _unitOWDb.Category.Update(categoryObj);
                _unitOWDb.Save();
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

            Category? categoryFromDB = _unitOWDb.Category.GetOne(r => r.Id == categoryId, includeProperties: "Category");

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? categoryId)
        {
            Category? category = _unitOWDb.Category.GetOne(r => r.Id == categoryId, includeProperties: "Category");
            if (category == null)
            {
                return NotFound();
            }
            _unitOWDb.Category.Delete(category);
            _unitOWDb.Save();
            TempData["success"] = "The category was deleted successfully";
            return RedirectToAction("Index", "Category");

        }


    }
}
