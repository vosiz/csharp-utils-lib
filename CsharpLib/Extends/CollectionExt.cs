using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Extends
{
    public static class CollectionExt
    {

        /// IEnumerable
        public static T RandomValue<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int size = collection.Count();
            if (collection.Count() == 0)
                throw new InvalidOperationException("Collection is empty");

            var list = collection as IList<T>;

            return list[Utils.Randomizer.Next(size)];
        }

        public static Type GetItemType<T>(this IEnumerable<T> list)
        {

            return list.GetType().GetGenericArguments().FirstOrDefault() ?? list.FirstOrDefault()?.GetType();
        }

        public static T[] AsArray<T>(this IEnumerable<T> list, params T[] items)
        {

            if (items.Length > 0)
                list.ToList().AddRange(items);

            return list.ToArray();
        }

        public static string Implode<T>(this IEnumerable<T> list, string separator = ", ") where T : IConvertible
        {
            return string.Join(separator, list);
        }

        /// dictionaries
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue val)
        {

            if (dict.ContainsKey(key))
                return false;

            dict.Add(key, val);
            return true;
        }

        public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {

            if (dict.ContainsKey(key))
                return dict[key];

            throw new IndexOutOfRangeException($"Key {key.ToString()} does not exist in dictionary");
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, bool if_notf_def = false)
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }

            if (if_notf_def)
                return default(TValue);

            throw new KeyNotFoundException($"Key '{key}' not found in dictionary.");
        }

        public static string Implode<TKey, TValue>(this Dictionary<TKey, TValue> dict, string separator = ", ")
        {
            return string.Join(separator, dict.Values);
        }

    }
}
