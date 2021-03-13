using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infrastructure.Helpers
{
	public static class EnumDescriptionHelper
	{
        public static string GetEnumDescription(this Enum value)
        {
            if (value == null)
                return string.Empty;
            var fi = value.GetType().GetField(value.ToString());
            if (fi != null &&
                fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes &&
                attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }

        public static ICollection<string> GetEnumDescriptions<T>()
        {
            var result = new List<string>();
            var type = typeof(T);
            if (!type.IsEnum)
                return result;
            result.AddRange(type.GetFields()
                .Select(field => Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)))
                .OfType<DescriptionAttribute>().Select(attribute => attribute.Description));
            return result;
        }

        public static T GetValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }
    }
}
