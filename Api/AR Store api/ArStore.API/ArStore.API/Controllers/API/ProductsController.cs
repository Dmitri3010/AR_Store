using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArStore.API.Db;
using ArStore.API.Models;
using ArStore.API.Models.API;
using ArStore.API.Repository;
using ArStore.API.Services;
using ArStore.API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArStore.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static string ProductsImagesFolderPath =>
            ConfigureServices.Get("ProductsImagesFolderPath").TrimStart('/').TrimStart('\\').TrimEnd('/').TrimEnd('\\');

        
        [HttpGet]
        public List<ProductsApiResponce> Get()
        {
            return SimpleRepository.Get<Product>().Select(t =>
            {
                var tempTemplate = ModelsMapper.Map<ProductsApiResponce>(t);                
                tempTemplate.ImageForTarget = Path.Combine(ProductsImagesFolderPath, t.ImageForTarget ?? "");
                tempTemplate.Model = Path.Combine(ProductsImagesFolderPath, t.Model ?? "");                
                tempTemplate.texturesObj = "";
                return tempTemplate;
            }).ToList();
        } 


    }

}