using System;
using Loby.Tools;
using System.Linq;
using System.Collections.Generic;

namespace Loby.Extensions
{
    public static class EnumerableExtensions
    {
        #region Value

        /// <summary>
        /// Indicates whether the specified list is null.
        /// </summary>
        /// <param name="list">
        /// The list to test.
        /// </param>
        /// <returns>
        /// true if the list parameter is null; otherwise, false.
        /// </returns>
        public static bool IsNull<T>(this IEnumerable<T> list)
        {
            return list == null;
        }

        /// <summary>
        /// Indicates whether the specified list is empty.
        /// </summary>
        /// <param name="list">
        /// The list to test.
        /// </param>
        /// <returns>
        /// true if the list parameter is empty; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// List is null.
        /// </exception>
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return list.Count() == 0;
        }

        /// <summary>
        /// Indicates whether the specified list is null or empty.
        /// </summary>
        /// <param name="list">
        /// The list to test.
        /// </param>
        /// <returns>
        /// true if the list parameter is null or empty; otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list.IsNull() || list.IsEmpty();
        }

        /// <summary>
        /// Shuffles the location of elements in a list.
        /// </summary>
        /// <typeparam name="T">
        /// The type of list objects.
        /// </typeparam>
        /// <param name="list">
        /// The list to be shuffled.
        /// </param>
        /// <returns>
        /// Returns a list whose elements are positioned differently
        /// from the first case.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// list is null.
        /// </exception>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(x => Randomizer.RandomInt());
        }

        #endregion;

        #region Paging

        /// <summary>
        /// Paginates a set of elements based on <paramref name="page"/> 
        /// and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Type of sequence elements.
        /// </typeparam>
        /// <param name="source">
        /// A set of elements to be paginated.
        /// </param>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// Number of elements per page.
        /// </param>
        /// <returns>
        /// A set of elements that are located on the specific page.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// source is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// page -or- pageSize is equal or less than 0.
        /// </exception>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source is null.");
            }

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException("page is equal or less than 0.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize is equal or less than 0.");
            }

            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        #endregion;

        #region Utils

        /// <summary>
        /// Combines all elements of the list with the specified separator.
        /// </summary>
        /// <param name="list">
        /// The list whose elements are going to be combined with the specified separator.
        /// </param>
        /// <param name="character">
        /// The character to be placed among the elements.
        /// </param>
        /// <returns>
        /// A new string instance created from a combination of specified list elements 
        /// and character among them. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// list is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// list contains no elements.
        /// </exception>
        public static string Join(this IEnumerable<string> list, char character)
        {
            return list.Aggregate((sentence, next) => next + character + sentence);
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the provided 
        /// key selector to compare values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of source.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by keySelector.
        /// </typeparam>
        /// <param name="source">
        /// The sequence to remove duplicate elements from.
        /// </param>
        /// <param name="keySelector">
        /// A function to extract the key for each element.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{TSource}"/> that contains distinct elements from the source sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// source is null.
        /// </exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        #endregion;
    }
}
