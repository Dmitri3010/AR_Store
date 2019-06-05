using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArStore.API.Db;
using ArStore.API.Models;
using ArStore.API.Models.API;
using ArStore.API.Services;
using ArStore.API.Tools;
using Microsoft.AspNetCore.Mvc;

namespace ArStore.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static string ProductsImagesFolderPath =>
            ConfigureServices.Get("ProductsImagesFolderPath").TrimStart('/').TrimStart('\\').TrimEnd('/').TrimEnd('\\');


        [HttpGet]
        public List<CategoriesApiResponce> Get()
        {
            return SimpleRepository.Get<Category>().Select(t =>
            {
                var category = ModelsMapper.Map<CategoriesApiResponce>(t);
                category.Image = Path.Combine(ProductsImagesFolderPath, t.Image ?? "");                
                return category;
            }).ToList();
        }
    }
}