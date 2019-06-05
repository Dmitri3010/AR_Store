using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArStore.API.Repository;
using ArStore.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArStore.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private static string ProductsImagesFolderPath =>
            ConfigureServices.Get("ModelImagesFolderPath").TrimStart('/').TrimStart('\\').TrimEnd('/').TrimEnd('\\');
        private readonly IHostingEnvironment _appEnvironment;
        public ModelController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public VirtualFileResult Get(int productId)
        {
            var product = Products.Get(productId);
            string pathToModel = Path.Combine(ProductsImagesFolderPath, product.Model ?? "");
            string fileType = "application/unity3d";
            string fileName = product.Model;
            return File(pathToModel, fileType, fileName);
        }
    }
}