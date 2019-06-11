using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArStore.API.Models;
using ArStore.API.Repository;
using Microsoft.AspNetCore.Http;
using System.IO;
using ArStore.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace ArStore.API.Controllers
{
    
    public class HomeController : Controller
    {
        private static string WebRootPath => Statics.WebRootPath;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult CategoriesList()
        {
            var categories = Categories.Get();
            ViewData["categories"] = categories;
            return View(categories);
        }

        [HttpGet]
        public IActionResult CategoriesAddOrUpdate(int id = -1)
        {
            var category = Categories.Get(id);            
            return View(category);
        }

        [HttpGet]
        public IActionResult ProductsList()
        {
            var products = Products.Get();
            return View(products);
        }

        [HttpGet]
        public IActionResult ProductsAddOrUpdate(int id = -1)
        {
            ViewData["Categories"] = Categories.Get();
            var product = Products.Get(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult ProductsAddOrUpdate(Product product, IFormFile[] files,  IFormFile model, IFormFile mainfile)
        {

            product.ImageForTarget = SaveFile(mainfile, "products") ?? product.ImageForTarget;
            product.Model = SaveFile(model, "products") ?? product.ImageForTarget;


            if (files.Any())
            {
                product.texturesObj = new List<string>();
                files.ToList().ForEach(f => product.texturesObj.Add(SaveFile(f, "products")));
                product.texturesObj = product.texturesObj.Where(i => i != null).ToList();
            }

            Products.AddOrUpdate(product);

            ViewData["Categories"] = Categories.Get();

            return RedirectToAction(nameof(ProductsList));
        }

        [HttpPost]
        public IActionResult CategoriesAddOrUpdate(Category category, IFormFile mainfile)
        {

            category.Image = SaveFile(mainfile, "categories") ?? category.Image;

            Categories.AddOrUpdate(category);

            ViewData[nameof(Categories)] = Categories.Get();

            return RedirectToAction(nameof(CategoriesList));
        }

        [HttpGet]
        public IActionResult CategoriesDelete(int id)
        {
            var category = Categories.Get(id);

            Categories.Delete(category);

            return RedirectToAction(nameof(CategoriesList));
        }

        [HttpGet]
        public IActionResult ProductsDelete(int id)
        {
            var product = Products.Get(id);

            Products.Delete(product);

            return RedirectToAction(nameof(ProductsList));
        }

        private static string SaveFile(IFormFile file, string folder)
        {
            if (file == null)
            {
                return null;
            }

            var fileName = $"{Guid.NewGuid()}__{file.FileName}";
            var saveImagePath = $"images/{folder}/{fileName}";
            var fullPath = Path.Combine(WebRootPath, saveImagePath);

            try
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                    return fileName;
                }
            }
            catch
            {
                return null;
            }
        }
        //private static string SaveModel(IFormFile file, string folder)
        //{
        //    if (file == null)
        //    {
        //        return null;
        //    }

        //    var fileName = $"{Guid.NewGuid()}__{file.FileName}";
        //    var saveImagePath = $"images/{folder}/{fileName}";
        //    var fullPath = Path.Combine(ContentRootPath, saveImagePath);

        //    try
        //    {
        //        using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        //        {
        //            file.CopyTo(fileStream);
        //            return fileName;
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
