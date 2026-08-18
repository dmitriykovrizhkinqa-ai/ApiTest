using Newtonsoft.Json;

namespace ApiTestFramework.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Проверяет, содержит ли строка подстроку (регистронезависимо)
        /// </summary>
        public static bool ContainsIgnoreCase(this string source, string value)
        {
            return source?.IndexOf(value, System.StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Преобразует объект в JSON-строку с форматированием
        /// </summary>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        /// <summary>
        /// Проверяет, что список не пустой
        /// </summary>
        public static bool IsNotEmpty<T>(this IEnumerable<T>? collection)
        {
            return collection != null && collection.Any();
        }

        /// <summary>
        /// Безопасное извлечение значения из словаря
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary, 
            TKey key, 
            TValue defaultValue = default!) where TKey : notnull
        {
            return CollectionExtensions.GetValueOrDefault(dictionary, key, defaultValue);
        }
    }
}