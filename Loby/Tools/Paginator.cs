using System.Linq;
using System.Collections.Generic;

namespace Loby.Tools
{
    /// <summary>
    /// Provides pagination for collections.
    /// </summary>
    public class Paginator
    {
        #region Members

        /// <summary>
        /// Any instance of this class holds the pagination result 
        /// by <see cref="Paginator"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Type of sequence elements.
        /// </typeparam>
        public class PagingResult<T>
        {
            public IEnumerable<T> Items { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }
        }

        #endregion;

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
        public static PagingResult<T> ApplyPaging<T>(IQueryable<T> source, int page, int pageSize)
        {
            int totalItems = source.Count();

            var result = new PagingResult<T>
            {
                CurrentPage = page,
                TotalItems = totalItems,
                Items = source.Skip((page - 1) * pageSize).Take(pageSize),
                TotalPages = (double)totalItems / pageSize % 1 == 0 ? totalItems / pageSize : totalItems / pageSize + 1
            };

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
        public static PagingResult<T> ApplyPaging<T>(IEnumerable<T> source, int page, int pageSize)
        {
            return ApplyPaging(source.AsQueryable(), page, pageSize);
        }
    }
}
