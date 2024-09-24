using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace LibraryApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {   private readonly IUnitOfWork _IUnitOfWorkDb;
        public ProductController(IUnitOfWork db)
        {
            _IUnitOfWorkDb = db;
        }
        public IActionResult Index()
        {   
            List<Product> listProduct = _IUnitOfWorkDb.Product.GetAll().ToList();
            return View(listProduct);
        }

        public IActionResult Create() 
        {
            IEnumerable<SelectListItem> CategoryList = _IUnitOfWorkDb.Category.GetAll().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;
            //ViewData[nameof(CategoryList)] = CategoryList;
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Product prodObj)
        {
            if (ModelState.IsValid) 
            {
                _IUnitOfWorkDb.Product.Add(prodObj);
                _IUnitOfWorkDb.Save();
                return RedirectToAction("Index", "Product");      
            }
            return View();
        }

        public IActionResult Edit(int? IdProduct) 
        { 
            if(IdProduct == 0 || IdProduct == null)
            {
                return NotFound();
            }
            Product? objProduct = _IUnitOfWorkDb.Product.GetOne(r => r.Id == IdProduct);
            if (objProduct == null)
            {
                return NotFound();
            }


            return View(objProduct); 
        
        }

        [HttpPost]
        public IActionResult Edit(Product prod) 
        {
            if((prod.ListPrice).GetType() != typeof(double)){
                prod.ListPrice = (double)(prod.ListPrice);
            }
            if ((prod.ListPrice30).GetType() != typeof(double))
            {
                prod.ListPrice30 = (double)(prod.ListPrice30);
            }
            if ((prod.ListPriceHigher30).GetType() != typeof(double))
            {
                prod.ListPriceHigher30 = (double)(prod.ListPriceHigher30);
            }

            if (ModelState.IsValid)
            {

                _IUnitOfWorkDb.Product.Update(prod);
                _IUnitOfWorkDb.Save();

                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Delete(int? IdProduct) 
        {
            if(IdProduct==0 || IdProduct == null)
            {
                return NotFound();
            }

            Product objProduct = _IUnitOfWorkDb.Product.GetOne(r =>r.Id == IdProduct);
            if (objProduct == null)
            {
                return NotFound();
            }
            return View(objProduct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Product objProduct)
        {
            if(objProduct == null)
            {
                return NotFound();  
            }

            _IUnitOfWorkDb.Product.Delete(objProduct);
            _IUnitOfWorkDb.Save();
            return RedirectToAction("Index", "Product");
        }


    }
}
