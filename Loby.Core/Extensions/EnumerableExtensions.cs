﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Loby.Core
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
