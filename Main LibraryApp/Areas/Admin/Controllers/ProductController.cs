using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.Models;
using Library.Models.ViewModels;
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

        public IActionResult Upsert(int? id) //Create + Update
        {

            ProductVM productVM = new()
            {
                CategoryList = _IUnitOfWorkDb.Category.GetAll().Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }),


                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _IUnitOfWorkDb.Product.GetOne(r => r.Id == id);
                return View(productVM);
            }

            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM prodObj, IFormFile? file) //It is now ProductVM not just Product. 
        {
            if (ModelState.IsValid) 
            {
                _IUnitOfWorkDb.Product.Add(prodObj.Product);
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
