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
        private readonly IWebHostEnvironment _WebHostEnvironment; //use it so we can save image in the right folder
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _IUnitOfWorkDb = db;
            _WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {   
            List<Product> listProduct = _IUnitOfWorkDb.Product.GetAll(includeProperties:"Category").ToList();
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
                productVM.Product = _IUnitOfWorkDb.Product.GetOne(r => r.Id == id, includeProperties: "Category");
                return View(productVM);
            }

            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM prodObj, IFormFile? file) //It is now ProductVM not just Product. 
        {//file comes from name in the view
            if (ModelState.IsValid) 
            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//creating name for file uploaded
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); //getting the route of the right folder

                    if (!string.IsNullOrEmpty(prodObj.Product.ImageURL)) //check if there is a imageUrl in the folder for this particular product
                    {
                        //delete old img
                        var oldImagePath = Path.Combine(wwwRootPath, prodObj.Product.ImageURL.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath)) //check if that img exists
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    prodObj.Product.ImageURL = @"/images/product/" + fileName;
                }
                if (prodObj.Product.Id == 0 || prodObj.Product.Id == null)
                {
                    _IUnitOfWorkDb.Product.Add(prodObj.Product);
                }
                else
                {
                    _IUnitOfWorkDb.Product.Update(prodObj.Product);
                }



                    _IUnitOfWorkDb.Save();
                TempData["success"] = "Product created successfully";
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

            Product objProduct = _IUnitOfWorkDb.Product.GetOne(r =>r.Id == IdProduct, includeProperties:"Category");
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
            TempData["success"] = "The product was deleted successfully";
            return RedirectToAction("Index", "Product");
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> listProduct = _IUnitOfWorkDb.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = listProduct});
        }

        #endregion

    }
}
