using System.ComponentModel;
using System.Reflection;
namespace Si.Framework.ToolKit.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            var type = enumValue.GetType();
            var name = Enum.GetName(type, enumValue);

            if (name != null)
            {
                var field = type.GetField(name);
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                return attribute?.Description ?? name; // 如果没有自定义名称，返回 enum 的名称
            }
            return "未知";
        }
        /// <summary>
        /// 获取枚举描述(类）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T obj, string propertyName = "") where T : class
        {
            if (obj == null) return "未知";

            Type type = obj.GetType();

            // 如果传入了属性名，获取属性上的 Description
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo property = type.GetProperty(propertyName);
                if (property != null)
                {
                    var attribute = property.GetCustomAttribute<DescriptionAttribute>();
                    return attribute?.Description ?? propertyName;
                }
            }
            else
            {
                var attribute = type.GetCustomAttribute<DescriptionAttribute>();
                return attribute?.Description ?? type.Name;
            }

            return "未知";
        }
        /// <summary>
        /// 根据描述获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetEnumFromDescribe<T>(this string propertyName) where T : Enum
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null && attribute.Description == propertyName)
                {
                    // 如果属性名称匹配，则返回相应的枚举值
                    return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException($"No matching enum value found for property name: {propertyName}", nameof(propertyName));
        }
        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value) ?? "未知";
        }
        public static T GetEnumFromName<T>(this string enumName) where T : struct
        {
            if (string.IsNullOrWhiteSpace(enumName))
            {
                throw new ArgumentException("Enum name cannot be null or empty", nameof(enumName));
            }
            if (Enum.TryParse(enumName, true, out T result))
            {
                return result;
            }

            throw new ArgumentException($"No matching enum value found for enum name: {enumName}", nameof(enumName));
        }

    }
}
