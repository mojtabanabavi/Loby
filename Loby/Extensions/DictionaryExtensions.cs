using System;
using System.Linq;
using System.Collections.Generic;

namespace Loby.Extensions
{
    /// <summary>
    /// A collection of extension methods for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Creates a reverse dictionary from <paramref name="source"/> that 
        /// the key and value are swapped.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of keys in the source dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of values in the source dictionary.
        /// </typeparam>
        /// <param name="source">
        /// A generic collection of key/value pairs.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="IDictionary{TValue, TKey}"/> created 
        /// from the <paramref name="source"/> that the key and value are swapped.
        /// </returns>
        /// <remarks>
        /// Since a dictionary can have multiple keys for one value, only the 
        /// first one will be used as key in this inversion.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The source is null.
        /// </exception>
        public static IDictionary<TValue, TKey> ReverseKeyValue<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var reversedKeyValueDictionary = new Dictionary<TValue, TKey>();

            foreach (var entry in source)
            {
                if (!reversedKeyValueDictionary.ContainsKey(entry.Value))
                {
                    reversedKeyValueDictionary.Add(entry.Value, entry.Key);
                }
            }

            return reversedKeyValueDictionary;
        }

        /// <summary>
        /// Determines whether the <paramref name="source"/> contains an element
        /// with the specified value.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of keys in the source dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of values in the source dictionary.
        /// </typeparam>
        /// <param name="source">
        /// A generic collection of key/value pairs.
        /// </param>
        /// <param name="value">
        /// The value to locate in source.
        /// </param>
        /// <returns>
        /// True if the source contains an element with the specified value; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The source is null.
        /// </exception>
        public static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TValue value)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Values.Contains(value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the first key associated with the specified value.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of keys in the source dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of values in the source dictionary.
        /// </typeparam>
        /// <param name="source">
        /// A generic collection of key/value pairs.
        /// </param>
        /// <param name="value">
        /// The value whose key to get.
        /// </param>
        /// <param name="key">
        /// When this method returns, the first key associated with the specified value, if the
        /// key is found; otherwise, the default value for the type of the key parameter.
        /// </param>
        /// <returns>
        /// True if the dictionary contains an element with the specified key; otherwise, false.
        /// </returns>
        public static bool TryGetFirstKey<TKey, TValue>(this IDictionary<TKey, TValue> source, TValue value, out TKey key)
        {
            try
            {
                if (source.ContainsValue(value))
                {
                    key = source.First(x => x.Value.Equals(value)).Key;

                    return true;
                }
            }
            catch
            {
            }

            key = default(TKey);

            return false;
        }
    }
}
