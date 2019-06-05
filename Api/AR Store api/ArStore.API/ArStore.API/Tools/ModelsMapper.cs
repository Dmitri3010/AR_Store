using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ArStore.API.Tools
{
    public static class ModelsMapper
    {
        private const BindingFlags AccessibleBindingFlags = BindingFlags.Public |
                                                            BindingFlags.Static |
                                                            BindingFlags.Instance |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.IgnoreCase;

        public static T Map<T>(object value) where T : class
        {
            if (value == null)
            {
                return null;
            }

            var returnObject = (T)Activator.CreateInstance(typeof(T));
            var sourceProps = value.GetType().GetProperties(AccessibleBindingFlags);
            foreach (var sourceProp in sourceProps)
            {
                var targetProp = typeof(T).GetProperty(sourceProp.Name, AccessibleBindingFlags);

                if (targetProp == null || targetProp.PropertyType != sourceProp.PropertyType)
                {
                    continue;
                }

                var sourceValue = sourceProp.GetValue(value);

                if (sourceValue == null)
                {
                    continue;
                }

                targetProp.SetValue(returnObject, sourceValue, null);
            }

            return returnObject;
        }
    }
}
