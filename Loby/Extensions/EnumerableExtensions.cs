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
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int pageSize)
        {
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

        #endregion;
    }
}
