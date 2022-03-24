using System;
using System.Linq;
using System.Linq.Expressions;

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

        #region ordering

        /// <summary>
        /// Sorts the elements of a sequence according to a expression.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the elements of source.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to order.
        /// </param>
        /// <param name="expression">
        /// An expression string that indicate how values should be order by.
        /// </param>
        /// <returns>
        /// A <see cref="IQueryable{T}"/> whose elements are sorted according to the 
        /// specified <paramref name="expression"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// source is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// expression is null.
        /// </exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string expression)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source is null.");
            }

            if (expression == null)
            {
                throw new ArgumentException("expression is null.");
            }

            var expressionItems = expression.Split(' ');

            var propertyName = expressionItems[0];
            var orderByMethod = expressionItems.Length == 1 ? "OrderBy" :
                (expressionItems[1].Equals("DESC", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy");

            var parameterExpression = Expression.Parameter(source.ElementType);
            var memberExpression = Expression.Property(parameterExpression, propertyName);

            var query = Expression.Call(
                typeof(Queryable),
                orderByMethod,
                new Type[] { source.ElementType, memberExpression.Type },
                source.Expression,
                Expression.Quote(Expression.Lambda(memberExpression, parameterExpression))
                );

            return source.Provider.CreateQuery(query) as IQueryable<T>;
        }

        #endregion;
    }
}
