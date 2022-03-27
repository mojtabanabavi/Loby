using System;
using Loby.Tools;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Loby.Tools.Models
{
    /// <summary>
    /// Any instance of this class holds the pagination result 
    /// and its metadata.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements of source.
    /// </typeparam>
    public class PagingResult<T>
    {
        /// <summary>
        /// A number that indicates the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// A number that indicates the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// A number that indicates the total number of items.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// A number that indicates the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Specifies whether current page is the last page or not.
        /// </summary>
        public bool IsLastPage { get; set; }

        /// <summary>
        /// Specifies whether current page is the first page or not.
        /// </summary>
        public bool IsFirstPage { get; set; }

        /// <summary>
        /// Specifies whether there is/are other page(s) after the current page.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// An <see cref="IEnumerable{T}"/> that contains the pagination result.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Specifies whether there is/are other page(s) befor the current page.
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// A number that indicates the number of filtered items.
        /// </summary>
        public int TotalFilteredItems { get; set; }
    }
}