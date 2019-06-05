using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArStore.API.Services
{
    public class Statics
    {
        public static IConfiguration Configuration { get; set; }

        public static IHostingEnvironment Environment { get; set; }

        public static string WebRootPath { get; set; }
    }
}
