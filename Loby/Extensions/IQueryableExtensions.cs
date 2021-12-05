using System;
using System.Linq;

namespace Loby.Extensions
{
    public static class IQueryableExtensions
    {
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
        public static IQueryable<T> Page<T>(this IQueryable<T> source, int page, int pageSize)
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
    }
}
