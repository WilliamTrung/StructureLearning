using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Extensions
{
    public static class EnumerableExtension
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string sortField, bool asc)
        {
            if (string.IsNullOrWhiteSpace(sortField))
            {
                return source;
            }

            var element = Expression.Parameter(source.ElementType, string.Empty);
            var property = Expression.Property(element, sortField);
            var lambda = Expression.Lambda(property, element);
            var methodName = asc ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
            var expression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(expression);
        }
    }
}
