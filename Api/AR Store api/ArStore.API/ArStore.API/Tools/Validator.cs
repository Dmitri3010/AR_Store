using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using ArStore.API.Helpers;
using Newtonsoft.Json;

namespace ArStore.API.Tools
{
    public static class Validator
    {
        public static void Validate<T>(T obj, Dictionary<string, string> rules = null) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            rules = rules ?? GetRules(obj);

            // get properties of input value
            var props = typeof(T).GetProperties(AccessibleBindingFlags);

            foreach (var prop in props)
            {
                // skip if rules not rule for this property in rules dictionary 
                if (!rules.TryGetValue(GetPropRuleName(prop), out var pattern)) continue;

                if (!AccessibleTypes.Contains(prop.PropertyType)) continue;

                var propValue = prop.GetValue(obj);

                // if value string try cast object to string
                // if result of cast null set empty string
                var stringValue = propValue?.ToString();

                if (Regex.IsMatch(stringValue, pattern)) continue;

                throw new ValidationException("value does not match pattern", prop.Name, propValue, pattern);
            }
        }

        public static Dictionary<string, string> GetRules<T>(T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return GetRules<T>();
        }

        public static Dictionary<string, string> GetRules<T>() where T : class
        {
            return GetRules(typeof(T));
        }

        public static Dictionary<string, string> GetRules(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var rules = new Dictionary<string, string>();

            var props = type.GetProperties(AccessibleBindingFlags);

            foreach (var prop in props)
            {
                var ruleAttr = prop.GetCustomAttribute<ValidationPatternAttribute>();
                if (ruleAttr == null) continue;

                var name = GetPropRuleName(prop);
                var rule = ruleAttr.Pattern;

                rules.Add(name, rule);
            }

            return rules;
        }

        private static string GetPropRuleName(MemberInfo prop)
        {
            var jsonNameAttr = prop.GetCustomAttribute<JsonPropertyAttribute>();
            return jsonNameAttr != null ? jsonNameAttr.PropertyName : prop.Name.ToLowerFirstChar();
        }

        private static readonly List<Type> AccessibleTypes = new List<Type>
        {
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(string),
            typeof(char)
        };

        private const BindingFlags AccessibleBindingFlags = BindingFlags.Public |
                                                            BindingFlags.Static |
                                                            BindingFlags.Instance |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.IgnoreCase;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidationPatternAttribute : Attribute
    {
        public ValidationPatternAttribute(string pattern)
        {
            Pattern = pattern;
        }

        public readonly string Pattern;
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message, string propertyName, object value, string pattern) : base(message)
        {
            ValidationValue = value;
            ValidationPattern = pattern;
            ValidationpPopertyName = propertyName;
        }

        public readonly object ValidationpPopertyName;
        public readonly object ValidationValue;
        public readonly string ValidationPattern;
    }
}