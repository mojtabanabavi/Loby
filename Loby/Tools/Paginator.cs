using System;
using System.Linq;
using System.Collections.Generic;

namespace Loby.Tools
{
    /// <summary>
    /// Provides pagination for collections.
    /// </summary>
    public class Paginator
    {
        public static readonly string Versian = "1.1";

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
        /// A set of elements that are located on the specific page 
        /// as <see cref="PagingResult{T}"/>, which provides more information 
        /// about pagination.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// source is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// page -or- pageSize is equal or less than 0.
        /// </exception>
        public static PagingResult<T> ApplyPaging<T>(IQueryable<T> source, int page, int pageSize)
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

            var totalItems = source.Count();

            var result = new PagingResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = source.Skip((page - 1) * pageSize).Take(pageSize),
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            };

            result.TotalFilteredItems = result.Items.Count();

            result.IsFirstPage = result.CurrentPage == 1;
            result.IsLastPage = result.CurrentPage == result.TotalPages;

            result.HasNextPage = result.CurrentPage < result.TotalPages;
            result.HasPreviousPage = result.CurrentPage > 1;

            return result;
        }

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
        /// A set of elements that are located on the specific page 
        /// as <see cref="PagingResult{T}"/>, which provides more information 
        /// about pagination.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// source is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// page -or- pageSize is equal or less than 0.
        /// </exception>
        public static PagingResult<T> ApplyPaging<T>(IEnumerable<T> source, int page, int pageSize)
        {
            return ApplyPaging(source.AsQueryable(), page, pageSize);
        }
    }
}
