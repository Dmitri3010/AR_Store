using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArStore.API.Services
{
    public class ConfigureServices
    {
        public static string Get(string name) => Statics.Configuration[name];

        public static T Get<T>(string name) => (T)Convert.ChangeType(Statics.Configuration[name], typeof(T));
    }
}
