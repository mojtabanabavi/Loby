using System;
using Loby.Tools;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Loby.Tools.Models
{
    /// <summary>
    /// Any instance of this class holds the 
    /// pagination result and its metadata.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements of source.
    /// </typeparam>
    public class PagingResult<T>
    {
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public bool IsLastPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool HasNextPage { get; set; }
        public IEnumerable<T> Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalFilteredItems { get; set; }
    }
}